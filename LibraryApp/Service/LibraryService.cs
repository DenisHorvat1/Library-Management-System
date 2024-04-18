using System;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Service
{
	public class LibraryService : ILibraryService
	{
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public LibraryService(IBookRepository bookRepository, IUserRepository userRepository, ITransactionRepository transactionRepository) 
		{
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetBookHistory(int bookId)
        {
            return await _transactionRepository.GetTransactionsForBookAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetRentedBooksAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            var rentedTransactions = transactions.Where(t => t.DateReturned == null);

            var distinctBookIds = rentedTransactions
                .Select(t => t.BookId)
                .Distinct();

            var rentedBooks = new List<Book>();
            foreach (var bookId in distinctBookIds)
            {
                var book = await _bookRepository.GetBookByIdAsync(bookId);
                if (book != null)
                {
                    rentedBooks.Add(book);
                }
            }

            return rentedBooks;

        }

        public async Task<IEnumerable<Transaction>> GetRentedBooksTransctionAsync() //can be improved
        {
            var transaction = await _transactionRepository.GetAllTransactionsAsync();

            return transaction.Where(t => t.DateReturned == null);
        }

        public async Task<IEnumerable<Transaction>> GetUserHistory(int userId) //can be improved
        {
            var transaction = await _transactionRepository.GetAllTransactionsAsync();

            return transaction.Where(t => t.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsersWithRentedBooksAsync()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            var rentedTransactions = transactions.Where(t => t.DateReturned == null);

            var userIds = rentedTransactions
                .Select(t => t.UserId)
                .Distinct();
            var users = new List<User>();
            foreach (var userId in userIds)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public Task<IEnumerable<Transaction>> GetUsersWithRentedTransactionBooksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RentBookAsync(int userId, int bookId)
        {
            //rentAbook
            //transaction
            await _bookRepository.RentABookAsync(bookId);

            var transaction = new Transaction
            {
                UserId = userId,
                BookId = bookId,
                DateBorrowed = DateTime.Now
            };

            await _transactionRepository.AddTransactionAsync(transaction);

        }

        public async Task ReturnBookAsync(int userId, int bookId)
        {
            await _bookRepository.ReturnABookAsync(bookId);

            var transactionUser = await _transactionRepository.GetTransactionsForUserAsync(userId);
            var transaction = transactionUser
                .Where(t => t.BookId == bookId)
                .Where(t => t.DateReturned == null)
                .FirstOrDefault();
            if (transaction == null)
            {
                throw new InvalidOperationException("The specified book was not borrowed by the user.");
            }

            transaction.DateReturned = DateTime.Now;
            await _transactionRepository.UpdateTransactionAsync(transaction);
        }
    }
}

