using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using System.Threading.Tasks;
using LibraryApp.Repositories;

namespace LibraryApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public TransactionController(ITransactionRepository transactionRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();

            return View(transactions);
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionRepository.GetTransactionByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public async Task<IActionResult> Create()
        {
            // Retrieve the list of users from the repository
            var users = await _userRepository.GetAllUsersAsync();
            var books = await _bookRepository.GetAllBooksAsync();

            // Create a SelectList containing user IDs and names
            var userIdSelectList = new SelectList(users, "Id", "Name");
            var bookIdSelectList = new SelectList(books, "Id", "Name");

            // Set the SelectList as ViewBag data
            ViewBag.UserId = userIdSelectList;
            ViewBag.BookId = bookIdSelectList;

            // Render the view for creating a transaction
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,UserId,DateBorrowed,DateReturned")] Transaction transaction)
        {
            // Retrieve the list of users from the repository
            var users = await _userRepository.GetAllUsersAsync();
            var books = await _bookRepository.GetAllBooksAsync();

            // Create a SelectList containing user IDs and names
            var userIdSelectList = new SelectList(users, "Id", "Name");
            var bookIdSelectList = new SelectList(books, "Id", "Name");

            // Set the SelectList as ViewBag data
            ViewBag.UserId = userIdSelectList;
            ViewBag.BookId = bookIdSelectList;
            if (ModelState.IsValid)
            {
                await _transactionRepository.AddTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Retrieve the list of users from the repository
            var users = await _userRepository.GetAllUsersAsync();
            var books = await _bookRepository.GetAllBooksAsync();

            // Create a SelectList containing user IDs and names
            var userIdSelectList = new SelectList(users, "Id", "Name");
            var bookIdSelectList = new SelectList(books, "Id", "Name");

            // Set the SelectList as ViewBag data
            ViewBag.UserId = userIdSelectList;
            ViewBag.BookId = bookIdSelectList;


            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionRepository.GetTransactionByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,UserId,DateBorrowed,DateReturned")] Transaction transaction)
        {
            // Retrieve the list of users from the repository
            var users = await _userRepository.GetAllUsersAsync();
            var books = await _bookRepository.GetAllBooksAsync();

            // Create a SelectList containing user IDs and names
            var userIdSelectList = new SelectList(users, "Id", "Name");
            var bookIdSelectList = new SelectList(books, "Id", "Name");

            // Set the SelectList as ViewBag data
            ViewBag.UserId = userIdSelectList;
            ViewBag.BookId = bookIdSelectList;

            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _transactionRepository.UpdateTransactionAsync(transaction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionRepository.GetTransactionByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionRepository.DeleteTransactionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
