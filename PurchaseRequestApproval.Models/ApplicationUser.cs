using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PurchaseRequestApproval.Models
{
   public class ApplicationUser : IdentityUser
    {
        // Access Level to be handled lower level for data entry and increase with admins
        public int AccessLevel { get; set; }




        // Forign Key Submitted by
        [Required]
        public int EmployeeUser { get; set; }
        [ForeignKey("EmployeeUser")]
        public Employee Employee { get; set; }


        // Role to be not mapped not to send to the data base
        [NotMapped]
        public String Role { get; set; }





    }
}
