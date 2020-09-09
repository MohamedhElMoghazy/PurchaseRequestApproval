using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.Models.ViewModels
{
    public class PRAQuoteVM
    {
        public PRAQuote PRAQuote { get; set; }
        public IEnumerable<SelectListItem> PRApprovalList { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> QuoteList { get; set; }
        public IEnumerable<SelectListItem> QuoteList1 { get; set; }
        public IEnumerable<SelectListItem> QuoteList2 { get; set; }
    }
}
