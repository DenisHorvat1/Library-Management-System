using System;
namespace LibraryApp.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public int UserId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }

        public Transaction()
		{
		}
	}
}

