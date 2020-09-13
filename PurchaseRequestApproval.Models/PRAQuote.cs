using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PurchaseRequestApproval.Models
{
    public class PRAQuote
    {
        [Key]
        public int Id { get; set; }
        public DateTime PRAQuoteDate { get; set; }

        public int PRARevision { get; set; }
        public string JustificationRev { get; set; }
        public double EstimatedPrice { get; set; }
        public double AddWarCost { get; set; }
        public double FreightCost { get; set; }
        public double EnvFees { get; set; }
        public double CarbonTax { get; set; }
        public double PSTCost { get; set; }
        public double Mobilization { get; set; }
        public double SiteOrientation { get; set; }
        public double RentalInsurance { get; set; }
        public double EquipmentDisinfection { get; set; }
        public double ContingencyPercentage { get; set; }
        public double ContingencyAmount { get; set; }
        public double Total { get; set; }


       
        // Forign Key for PR Approval
        [Required]
        public int PRAId { get; set; }
        [ForeignKey("PRAId")]
        public PRApproval PRApproval { get; set; }
        
       // Forign Key Submitted by
       [Required]
        public int SubmittedBy { get; set; }
        [ForeignKey("SubmittedBy")]
        public Employee Employee { get; set; }



        // Forign key for Quote ID
        [Required]
       public int QuoteId { get; set; }
       [ForeignKey("QuoteId")]
       public Quote Quote { get; set; }

        public int? QuoteAl1Id { get; set; }
        [ForeignKey("QuoteAl1Id")]
        public Quote QuoteAl1 { get; set; }

        public int? QuoteAl2Id { get; set; }
        [ForeignKey("QuoteAl2Id")]
        public Quote QuoteAl2 { get; set; }







    }














}
