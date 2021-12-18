using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancialAidAllocationTool.Models.Ledger
{
    public partial class DonationLedger
    {
        public int TransactionId { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TransactionDate { get; set; }       
         [Range(1, double.MaxValue, 
        ErrorMessage = "Credit must be positve non zero value.")] 
        public double? Credit { get; set; }
        [Range(1, double.MaxValue, 
        ErrorMessage = "Debit must be positve non zero value.")] 
        public double? Debit { get; set; }
        public string Memo { get; set; }
        
    }
}
