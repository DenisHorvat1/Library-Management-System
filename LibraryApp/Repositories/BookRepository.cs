using System;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repositories
{
	public class BookRepository : IBookRepository
	{
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
		{
            _context = context;
        }


        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Book
                .OrderByDescending(b => b.NumberOfCopies)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task AddBookAsync(Book book)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.ISBN == book.ISBN);

            if (existingBook != null)
            {
                // If book with the same ISBN already exists, increase NumberOfCopies
                existingBook.NumberOfCopies++;
                _context.Update(existingBook);
            }
            else
            {
                // If book with the same ISBN doesn't exist, add a new book
                book.NumberOfCopies = 1; // Set NumberOfCopies to 1 for new book
                _context.Add(book);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Book.AnyAsync(b => b.Id == id);
        }

        public async Task RentABookAsync(int id) //needs rework
        {

            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                throw new InvalidOperationException("The book is not available in this library");
            if (book.NumberOfCopies == 0)
                throw new InvalidOperationException("The book is not available momentanly");

            book.NumberOfCopies--;
            await UpdateBookAsync(book);

        }

        public async Task ReturnABookAsync(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                throw new InvalidOperationException("The book is not available in this library");

            book.NumberOfCopies++;
            await UpdateBookAsync(book);
        }
    }
}

