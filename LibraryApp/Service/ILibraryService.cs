using System;
using LibraryApp.Models;

namespace LibraryApp.Service
{
	public interface ILibraryService
	{
        Task RentBookAsync(int userId, int bookId);
        Task ReturnBookAsync(int userId, int bookId);

        Task<IEnumerable<Book>> GetRentedBooksAsync(); //now are rented
        Task<IEnumerable<User>> GetUsersWithRentedBooksAsync();

        Task<IEnumerable<Transaction>> GetUserHistory(int userId); //history of all their transaction
        Task<IEnumerable<Transaction>> GetBookHistory(int bookId);

        Task<IEnumerable<Transaction>> GetRentedBooksTransctionAsync(); //now are rented
        Task<IEnumerable<Transaction>> GetUsersWithRentedTransactionBooksAsync();
    }
}

