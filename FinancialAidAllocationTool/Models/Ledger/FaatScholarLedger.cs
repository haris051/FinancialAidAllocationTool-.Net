using System;
using System.Collections.Generic;

namespace FinancialAidAllocationTool.Models.Ledger
{
    public partial class FaatScholarLedger
    {
        public FaatScholarLedger()
        {
            FaatScholarLog = new HashSet<FaatScholarLog>();
        }

        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Memo { get; set; }

        public virtual ICollection<FaatScholarLog> FaatScholarLog { get; set; }
    }
}
