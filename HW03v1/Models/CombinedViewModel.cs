using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW03v1.Models
{
    public class CombinedViewModel
    {
       
        public IEnumerable<books> book { get; set;}
        public IEnumerable<students> student { get; set; }
        public IEnumerable<authors> author { get; set; }

        public IEnumerable<borrows> borrow { get; set; }
        public IEnumerable<types> type { get; set; }
       
    }
}