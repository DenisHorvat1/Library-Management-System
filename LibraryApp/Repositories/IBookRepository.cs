using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface IBookRepository
	{
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<bool> BookExistsAsync(int id);

        Task RentABookAsync(int id);
        Task ReturnABookAsync(int id);
    }
}

