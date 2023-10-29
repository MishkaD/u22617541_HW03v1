using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using HW03v1.Models;

namespace HW03v1.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities1 db = new LibraryEntities1();

        // Home page
        public ActionResult Index(int? page)
        {
            int pageSize = 20; // number of items to display per page.
            int pageNumber = (page ?? 1); // Get the current page number from the URL parameter or default to 1.

            var viewModel = new CombinedViewModel
            {
                author = db.authors.ToList(),
                borrow = db.borrows.ToList(),
                type = db.types.ToList(),
            };

            // Pagination for student
            var studentsList = db.students.OrderBy(s => s.studentId);
            var studentsPaged = studentsList.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.TotalStudentPages = (int)Math.Ceiling((double)studentsList.Count() / pageSize);
            ViewBag.CurrentStudentPage = pageNumber;

            viewModel.student = studentsPaged.ToList();

            // Pagination for book
            var bookList = db.books.Include("authors").Include("types").OrderBy(b => b.bookId);
            var bookListPaged = bookList.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.TotalBookPages = (int)Math.Ceiling((double)bookList.Count() / pageSize);
            ViewBag.CurrentBookPage = pageNumber;

            viewModel.book = bookListPaged.ToList();

            return View(viewModel);
        }

        // Maintain page
        public ActionResult About(int? authorPage, int? borrowPage, int? typesPage)
        {
            int pageSize = 10;
            int authorPageNumber = authorPage ?? 1;
            int borrowPageNumber = borrowPage ?? 1;
            int typesPageNumber = typesPage ?? 1;

            var viewModel = new CombinedViewModel
            {
                // Fetch data for the Authors table
                author = db.authors.ToList().Skip((authorPageNumber - 1) * pageSize).Take(pageSize).ToList(),

                // Fetch data for the Borrows table
                borrow = db.borrows.ToList().Skip((borrowPageNumber - 1) * pageSize).Take(pageSize).ToList(),

                // Fetch data for the Types table
                type = db.types.ToList().Skip((typesPageNumber - 1) * pageSize).Take(pageSize).ToList()
            };

            ViewBag.TotalAuthorPages = (int)Math.Ceiling((double)db.authors.Count() / pageSize);
            ViewBag.CurrentAuthorPage = authorPageNumber;

            ViewBag.TotalBorrowPages = (int)Math.Ceiling((double)db.borrows.Count() / pageSize);
            ViewBag.CurrentBorrowPage = borrowPageNumber;

            ViewBag.TotalTypesPages = (int)Math.Ceiling((double)db.types.Count() / pageSize);
            ViewBag.CurrentTypesPage = typesPageNumber;

            return View(viewModel);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}