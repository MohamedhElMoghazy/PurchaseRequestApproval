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
        public int AccessLevel { get; set; } // Disabled for now

        // Add name for Registeration
        public string Name { get; set; }




        // Forign Key Submitted by
        // [Required] Let the Employee User as option to enable viewing the data from outside
        public int? EmployeeUser { get; set; }
        [ForeignKey("EmployeeUser")]
        public Employee Employee { get; set; }


        // Role to be not mapped not to send to the data base
        [NotMapped]
        public String Role { get; set; }





    }
}
