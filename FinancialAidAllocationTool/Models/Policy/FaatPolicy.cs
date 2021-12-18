using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;




namespace FinancialAidAllocationTool.Models.Policy
{
    public partial class FaatPolicy
    {
        public FaatPolicy()
        {
            //IsSelected= 0;
            FaatRule = new HashSet<FaatRule>();
        }
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 4)]
        public double? MeritMinCGPA {get;set;}
        public double? NeedMinCGPA {get;set;}
        public int? IsSelected { get; set; }
        [RuleOrder]
        public virtual ICollection<FaatRule> FaatRule { get; set; }
    }
}
