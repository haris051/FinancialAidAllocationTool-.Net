using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace FinancialAidAllocationTool.Models.IntakeSeason
{
    public partial class FaatScholarshipStatus
    {
        public int Id { get; set; }
        public int? IntakeSeasonId { get; set; }
        public string Type { get; set; }
       
        public string Status { get; set; }
        public string Policy { get; set; }

        public virtual FaatIntakeSeason IntakeSeason { get; set; }
    }
}
