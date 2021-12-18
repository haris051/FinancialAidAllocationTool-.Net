using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Mvc;



namespace FinancialAidAllocationTool.Models.IntakeSeason
{
    public partial class FaatIntakeSeason 
    {
        public FaatIntakeSeason()
        {
            FaatScholarshipStatus = new HashSet<FaatScholarshipStatus>();
        }

        public int Id { get; set; }
        public int Year { get; set; }
        //[Intake_Season("Year")]
        public string IntakeSeason { get; set; }

        public DateTime InsertionTimestamp {get;set;}

        public virtual ICollection<FaatScholarshipStatus> FaatScholarshipStatus { get; set; }
    }
}
