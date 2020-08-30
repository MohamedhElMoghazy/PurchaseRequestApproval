using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class Vendor
    {
        // create entity for Vendor
        [Key] // annonce a key value

        public int Id { get; set; } // Id of purchasing type
        [Display(Name = "Vendor Name")]
        [Required]
        [MaxLength(50)]
        public String VendorName { get; set; } // Name of Vendor

        [Display(Name = "Vendor Code")]
        public String VendoreCode { get; set; } // The Code of the Vendor in Sage

        [Display(Name = "Vendor Address")]
        public String VendorAddress { get; set; } // The Address of the vendor 

        [Display(Name = "Vendor Phone")]
        [Required]
        
        public String VendorPhone { get; set; } // The phone of the Vendor in Sage

        [Display(Name = "Sales Contact Name")]
        [Required]
        public String SalesContactName { get; set; } // The contact for sales

        [Display(Name = "Sales Contact Email")]
        public String SalesContactEmail { get; set; } // The Sales Contact Email

        [Display(Name = "Accountant Contact Name")]
        public String AccountContactName { get; set; } // The accontant Contact Name

        [Display(Name = "Accountant Contact Email")]
        public String AccContactEmail{ get; set; } // The accontant Contact Email 

        [Display(Name = "Registered Vendor")]
        public bool RegVendor { get; set; } // State if the vendor is registered in the system or no 









    }
}
