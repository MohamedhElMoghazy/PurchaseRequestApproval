using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class Shipping
    {
        [Key] // annonce a key value
        public int Id { get; set; }

        // annonce a display name and it is required 
        [Display(Name ="ShippingName")]
        [Required]
        [MaxLength(50)]
        public string ShippingName { get; set; }
        [Display(Name = "ShippingDescription")]
        public string ShippingDescription { get; set; }
        [Display(Name = "ShippingACC")]
        public string ShippingACC { get; set; }

    }
}
