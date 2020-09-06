using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class VendorContact
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string  Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string  Branch { get; set; }
        public string Remark { get; set; }
        [Required]
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }



    }
}
