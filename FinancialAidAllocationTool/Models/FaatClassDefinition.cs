using System;
using System.Collections.Generic;
using FinancialAidAllocationTool.Models.Application;

namespace FinancialAidAllocationTool.Models
{
    public partial class FaatClassDefinition
    {
        public FaatClassDefinition()
        {
            FaatApplications = new HashSet<FaatApplication>();
            FaatScholarLog = new HashSet<FaatScholarLog>();

        }
        public int Id { get; set; }
        public string Discipline { get; set; }
        public string Semester { get; set; }
        public string SemesterCount { get; set; }
        public string Section { get; set; }
        public int? ClassStrength { get; set; }
        public virtual ICollection<FaatApplication> FaatApplications { get; set; }
        public virtual ICollection<FaatAppComments> FaatAppComments { get; set; }
        public virtual ICollection<FaatScholarLog> FaatScholarLog { get; set; }
    }
}
