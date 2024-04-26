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
using LibraryApp.Service;
using System.Threading.Tasks;

namespace LibraryApp.Controllers
{
    public class LibraryServiceController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryServiceController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index()
        {

            var rentedBooks = await _libraryService.GetRentedBooksAsync();
            var usersWithBooks = await _libraryService.GetUsersWithRentedBooksAsync();

            var allBooks = await _libraryService.GetAvailableBooksAsync();
            var allUsers = await _libraryService.GetAllUsersAsync();

            var allBooksIdSelectList = new SelectList(allBooks, "Id", "Name");
            var allUsersIdSelectList = new SelectList(allUsers, "Id", "Name");

            var userIdSelectList = new SelectList(usersWithBooks, "Id", "Name");
            var bookIdSelectList = new SelectList(rentedBooks, "Id", "Name");

            // Set the SelectList as ViewBag data for returning
            ViewBag.UsersReturn = userIdSelectList;
            ViewBag.BooksReturn = bookIdSelectList;

            //rent
            ViewBag.UsersRent = allUsersIdSelectList;
            ViewBag.BooksRent = allBooksIdSelectList;

            
            return View(rentedBooks);
        }


        public async Task<IActionResult> Details()
        {
            var users = await _libraryService.GetUsersWithRentedBooksAsync();
            var userBookPairs = new List<Tuple<User, IEnumerable<Book>>>();
            var userTransactionPairs = new List<Tuple<User, IEnumerable<Transaction>>>();

            foreach (var user in users)
            {
                var transactions = (await _libraryService.GetUserHistory(user.Id))
                    .Where(t => t.DateReturned == null);
                var rentedBooks = await _libraryService.GetRentedBooksForUserAsync(user.Id);
                userBookPairs.Add(new Tuple<User, IEnumerable<Book>>(user, rentedBooks));
                userTransactionPairs.Add(new Tuple<User, IEnumerable<Transaction>>(user, transactions));
            }

            return View(userTransactionPairs);
        }

        public async Task<IActionResult> RentedBooks()
        {
            var rentedBooks = await _libraryService.GetRentedBooksAsync();

            return View(rentedBooks);
        }

        public async Task<IActionResult> Search()
        {
            return View();
        }

        public async Task<IActionResult> SearchByName(string query)
        {
            var searchResults = await _libraryService.SearchBooks(query);
            return View(searchResults);
        }


        [HttpPost]
        public async Task<IActionResult> RentBook(int userId, int bookId)
        {
            await _libraryService.RentBookAsync(userId, bookId);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> ReturnBook(int userId, int bookId)
        {
            await _libraryService.ReturnBookAsync(userId, bookId);
            return RedirectToAction(nameof(Index));
        }


    }
}
