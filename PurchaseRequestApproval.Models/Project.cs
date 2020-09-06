using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string WorkPackageIn { get; set; }
        [Required]
        public string ContractNo { get; set; }



    }
}
