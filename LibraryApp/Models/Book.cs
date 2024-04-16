using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public string Genre { get; set; }
		public int NumberOfCopies { get; set; }

		public Book()
		{

		}
	}
}

