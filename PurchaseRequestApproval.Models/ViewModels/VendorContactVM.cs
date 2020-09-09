using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.Models.ViewModels
{
    public class VendorContactVM
    {
        public VendorContact VendorContact { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
    }
}
