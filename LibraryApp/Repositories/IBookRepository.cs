using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface IBookRepository
	{
		void AddBook(Book book);
		void UpdateBook(Book book);
		void DeleteBook(string isbn);
		Book GetBookByISBN(string isbn);
	}
}

