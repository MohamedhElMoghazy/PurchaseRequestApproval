using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class PurchaseType
    {
        // create entity for purchse type ( Material , Subcontractors, Equipment rental)
        [Key] // annonce a key value

        public int Id { get; set; } // Id of purchasing type
        [Display(Name = "Purchase Type")]
        [Required]
        [MaxLength(50)]
        public String PurcahseTypeName { get; set; } // Name of purchase type

        [Display(Name = "Purchase Code")]
        [Required]
        public int PurcahseCode { get; set; } // Code of Purchase Type



    }
}
