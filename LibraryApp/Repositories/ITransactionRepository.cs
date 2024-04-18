using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface ITransactionRepository
	{
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<bool> TransactionExistsAsync(int id);

        Task<IEnumerable<Book>> GetRentedBooksForUserAsync(int userId);
        Task<IEnumerable<User>> GetUsersForBookAsync(int bookId);

        Task<IEnumerable<Transaction>> GetTransactionsForBookAsync(int bookId);
        Task<IEnumerable<Transaction>> GetTransactionsForUserAsync(int userId);
    }
}

