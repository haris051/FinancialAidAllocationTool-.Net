using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 
using System.ComponentModel.DataAnnotations;


namespace FinancialAidAllocationTool.Models.Policy
{
    public partial class FaatRuleDescription
    {
        
        [Key]
        public int Id { get; set; }
        
        public int? RuleId { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Student No must be greater than 0")]

        public int? StudentNo { get; set; }
        [Range(1,double.MaxValue, ErrorMessage = "Please Give the Valid Amount")]

        public double Amount { get; set; }

        public virtual FaatRule Rule { get; set; }
    }
}
