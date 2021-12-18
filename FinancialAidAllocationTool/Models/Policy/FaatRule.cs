using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 




namespace FinancialAidAllocationTool.Models.Policy
{
    public partial class FaatRule
    {
        public FaatRule()
        {
            FaatRuleDescription = new HashSet<FaatRuleDescription>();
            
        }
        
        [Key]
        public int Id { get; set; }
        
        public int? PolicyId { get; set; }
        public int? Strength { get; set; }
       // [RuleDescription("Top", ErrorMessage = "Rules Description must be equal to Top")]
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Top must be greater than 0")]

        public int Top { get; set; }

        public virtual FaatPolicy Policy { get; set; }
        [RuleDescription("Top")]
        [RuleDescriptionOrder]

        
        public virtual ICollection<FaatRuleDescription> FaatRuleDescription { get; set; }
    }
}
