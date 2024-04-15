using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public class BookRepository : IBookRepository
	{
		public BookRepository()
		{
		}

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book GetBookByISBN(string isbn)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}

