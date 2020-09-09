using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace PurchaseRequestApproval.Models.ViewModels
{
   public class PRApprovalVM
    {
        public PRApproval PRApproval { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> PurchaseTypeList { get; set; }
        public IEnumerable<SelectListItem> ProjectList { get; set; }

    }
}
