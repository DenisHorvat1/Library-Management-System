using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetRentedBooksForUserAsync(int userId)
        {
            var rentedBookIds = await _context.Transaction
                .Where(t => t.UserId == userId)
                .Select(t => t.BookId)
                .ToListAsync();

            return await _context.Book
                .Where(b => rentedBookIds.Contains(b.Id))
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transaction.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersForBookAsync(int bookId)
        {
            // Find the book with the given ISBN
            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return Enumerable.Empty<User>(); // No users if the book doesn't exist
            }

            // Find transactions for the book
            var transactionUserIds = await _context.Transaction
                .Where(t => t.BookId == book.Id)
                .Select(t => t.UserId)
                .Distinct()
                .ToListAsync();

            // Find users based on the transaction user IDs
            var users = await _context.User
                .Where(u => transactionUserIds.Contains(u.Id))
                .ToListAsync();

            return users;
        }

        public async Task<bool> TransactionExistsAsync(int id)
        {
            return await _context.Transaction.AnyAsync(t => t.Id == id);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsForBookAsync(int bookId)
        {
            return await _context.Transaction
                .Where(t => t.BookId == bookId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsForUserAsync(int userId)
        {
            return await _context.Transaction
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}
