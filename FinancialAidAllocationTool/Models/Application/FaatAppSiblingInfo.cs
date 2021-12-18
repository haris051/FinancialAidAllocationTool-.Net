using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 
using System.ComponentModel.DataAnnotations;


namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppSiblingInfo
    {
        public FaatAppSiblingInfo()
        {
            FaatAppSibJobHolder = new HashSet<FaatAppSibJobHolder>();
            FaatAppSibStudent = new HashSet<FaatAppSibStudent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        [Display(Name="No of Siblings")]
        public int? NoOfSibling { get; set; }
    
        public virtual FaatApplication Application { get; set; }
        public virtual ICollection<FaatAppSibJobHolder> FaatAppSibJobHolder { get; set; }
        public virtual ICollection<FaatAppSibStudent> FaatAppSibStudent { get; set; }
    }
}
