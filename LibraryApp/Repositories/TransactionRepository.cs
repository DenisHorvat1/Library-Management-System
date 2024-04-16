using System;
using LibraryApp.Models;

namespace LibraryApp.Repositories
{
	public class TransactionRepository : ITransactionRepository
	{
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public TransactionRepository()
		{
		}

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void DeleteTransaction(int transactionId)
        {
            var transactionToDelete = _transactions.FirstOrDefault(u => u.Id == transactionId);
            if (transactionToDelete != null)
            {
                _transactions.Remove(transactionToDelete);
            }
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return _transactions.FirstOrDefault(u => u.Id == transactionId);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            var existingTransaction = _transactions.FirstOrDefault(u => u.Id == transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.BookId = transaction.BookId;
                existingTransaction.UserId = transaction.UserId;
                // Update other properties as needed
            }
        }
    }
}

