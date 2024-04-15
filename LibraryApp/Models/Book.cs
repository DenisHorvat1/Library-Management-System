using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
		public bool IsAvailable { get; set; }
		public string Borrower { get; set; }

		public Book()
		{

		}
	}
}

