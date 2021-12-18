using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinancialAidAllocationTool.Models.Application;
using FinancialAidAllocationTool.Models.Ledger;

namespace FinancialAidAllocationTool.Models
{
    public partial class FaatScholarLog
    {
        public int Id { get; set; }
        public int? Tid { get; set; }

        public int? ApplicationId { get; set; }
        public string AridNo { get; set; }

        public string Name { get; set; }
        //public DateTime? Date { get; set; }
        public int? ClassId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public double? Cgpa { get; set; }       
        public bool IsManual { get; set; }        
        public double? AllocationAmount { get; set; }
        public double? DefaultAmount {get;set;}
        public DateTime? InsertionTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }


        public virtual FaatScholarLedger T { get; set; }

        public virtual FaatApplication A { get; set; }

        public virtual FaatClassDefinition CD { get; set; }
    }
}
