using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW03v1.Models
{
    public class ReportModel
    {
        public string StudentName { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime BroughtDate { get; set; }
        public DateTime TakenDate { get; set; }
    }
}