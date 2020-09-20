using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PurchaseRequestApproval.Models
{
   public class PRApproval
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PRApprovalId { get; set; } // PRApproval Id should be created by the system and can not be modified
        public string PRApprovalTitle { get; set; } // title of the PR Approval
        public string PRApprovalDescription { get; set; }
        public string WorkOrder { get; set; }
        public Boolean CMOA { get; set; }
        public Boolean MatCorVer { get; set; }
        public Boolean Warranty { get; set; }
        public int WorkDurationSiteDays { get; set; }
        public Boolean RateSheet { get; set; }
        public Boolean GateAccess { get; set; }
        public int RentalPeriodDays { get; set; }
        public Boolean EquipmentTireEngine { get; set; }
        public string JustificationVendor { get; set; }
        public string ExcelFileUrl { get; set; }// Add file location into the database







        // Forign Key Vendor
        [Required]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        // Forign Key Purchase Type
        [Required]
        public int PurchaseTypeId { get; set; }
        [ForeignKey("PurchaseTypeId")]
        public PurchaseType PurchaseType { get; set; }

        // Forign Key Sourced by

        [Required]
        public int SourcedBy { get; set; }
        [ForeignKey("SourcedBy")]
        public Employee Employee { get; set; }

        // Forign Key Project ID
        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }




    }
}
