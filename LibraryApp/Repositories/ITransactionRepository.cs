using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public interface ITransactionRepository
	{
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);
        Transaction GetTransactionById(int transactionId);
    }
}

