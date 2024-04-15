using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
		public TransactionRepository()
		{
		}

        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DeleteTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransactionById(int transactionId)
        {
            throw new NotImplementedException();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}

