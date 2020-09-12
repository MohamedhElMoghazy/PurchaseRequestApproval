using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PurchaseRequestApproval.Models
{
   public class Quote
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string QuoteDescription { get; set; }


        [Required]
        public double QuoteAmount { get; set; }

        public Boolean SiteCompliant { get; set; }
        public Boolean Less3K { get; set; }
        public Boolean SoleProvider { get; set; }
        public Boolean OEM { get; set; }
        public Boolean ScheduleDrivenPur { get; set; }

        public Boolean Commonality { get; set; }
        public Boolean FirstNation { get; set; }
        public Boolean Metis { get; set; }
        public Boolean LocalVendor { get; set; }





        public int ETADays { get; set; }

       [DataType(DataType.Date)]
       [Display(Name = "Choose the quote data")]
       


        public DateTime QuoteDate { get; set; }
        public string PdfUrl { get; set; }


        // Add Forign Key for The Vendor    
        [Required ]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        // Add Forign Key for The Vendor Contact 
        [Required]
        public int VendorContactId { get; set; }
        [ForeignKey("VendorContactId")]
        public VendorContact VendorContact { get; set; }

        // Add Shipment Type
        [Required]
        public int ShippingId { get; set; }
        [ForeignKey("ShippingId")]
        public Shipping Shipping { get; set; }




    }
}
