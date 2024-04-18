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

            var books = await _libraryService.GetRentedBooksAsync();
            var users = await _libraryService.GetUsersWithRentedBooksAsync();
            ViewBag.Users = users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name });
            ViewBag.Books = books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RentBook(int userId, int bookId)
        {
            await _libraryService.RentBookAsync(userId, bookId);
            return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> ReturnBook(int userId, int bookId)
        {
            await _libraryService.ReturnBookAsync(userId, bookId);
            return View("Index");
        }
    }
}
