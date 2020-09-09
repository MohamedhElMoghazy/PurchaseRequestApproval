using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.Models.ViewModels
{
   public class QuoteVM
    {
        public Quote Quote { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> VendorContactList { get; set; }
        public IEnumerable<SelectListItem> ShippingList { get; set; }
        

    }
}
