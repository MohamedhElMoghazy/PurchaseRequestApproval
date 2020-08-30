using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class Employee
    {
        // create entity for Vendor
        [Key] // annonce a key value

        public int Id { get; set; } // Id of Employee
        [Display(Name = "Employee Name")]
        [Required]
        [MaxLength(50)]
        public String EmployeeName { get; set; } // Name of Employee

        [Display(Name = "Employee Code")]
        public String EmployeeCode { get; set; } // The Code of the Employee

        [Display(Name = "Employee Position")]
        public String EmployeePosition { get; set; } // The Position of the Employee 


        [Display(Name = "Employee Phone")]
        [Required]
        
        public String EmployeePhone { get; set; } // The phone of the Employee

        [Display(Name = "Employee Email")]
        [Required]
        public String EmployeeEmail { get; set; } // The Employee Email

        [Display(Name = "Employee Site")]
        [Required]
        public String EmployeeSite { get; set; } // The Employee Site



    }
}
