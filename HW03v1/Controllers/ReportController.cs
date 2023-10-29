using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HW03v1.Models;

namespace HW03v1.Controllers
{
    public class ReportController : Controller
    {
        private LibraryEntities1 db = new LibraryEntities1();

        public ActionResult Index()
        {
            // Populate the types dropdown in your view
            var viewModel = new CombinedViewModel
            {
                book = db.books.ToList(),
                student = db.students.ToList(),
                author = db.authors.ToList(),
                borrow = db.borrows.ToList(),
                type = db.types.ToList()
            };

            return View(viewModel);
        }

        public ActionResult GenerateReport(int? typeId)
        {
            if (!typeId.HasValue)
            {
                // Handle invalid typeId
                return RedirectToAction("Index");
            }

            // Determine which report to generate based on the selected typeId
            IEnumerable<ReportModel> report = null;

            switch (typeId)
            {
                case 1: // Current Loans Report
                    report = GenerateCurrentLoansReport();
                    break;
                case 2: // Overdue Books Report
                    report = GenerateOverdueBooksReport();
                    break;
                case 3: // Borrowing History Report (requires studentId)
                    int studentId = 1; 
                    report = GenerateBorrowingHistoryReport(studentId);
                    break;
                default:
                    // Handle invalid typeId
                    return RedirectToAction("Index");
            }
           
            var chartData = new
            {
                labels = report.Select(r => r.StudentName).ToList(),
                data = report.Select(r => r.BookTitle.Length).ToList() 
            };

            return View("Contact", chartData);

        }


    
        public IEnumerable<ReportModel> GenerateCurrentLoansReport()
        {
            var report = db.borrows
                .Where(b => b.BroughtDate == null) 
                .Select(b => new ReportModel
                {
                    StudentName = b.students.name,
                    BookTitle = b.books.name,
                    BorrowDate = b.TakenDate,
                    ReturnDate = null 
                })
                .ToList();
            return report;
        }

        public IEnumerable<ReportModel> GenerateOverdueBooksReport()
        {
            var currentDate = DateTime.Now;
            var report = db.borrows
                .Where(b => b.BroughtDate == null && b.TakenDate < currentDate) 
                .Select(b => new ReportModel
                {
                    StudentName = b.students.name,
                    BookTitle = b.books.name,
                    BorrowDate = b.TakenDate,
                    ReturnDate = null 
                })
                .ToList();
            return report;
        }

        public IEnumerable<ReportModel> GenerateBorrowingHistoryReport(int studentId)
        {
            var report = db.borrows
                .Where(b => b.studentId == studentId)
                .Select(b => new ReportModel
                {
                    StudentName = b.students.name,
                    BookTitle = b.books.name,
                    BorrowDate = b.TakenDate,
                    ReturnDate = b.BroughtDate
                })
                .ToList();
            return report;
        }

    }
}
