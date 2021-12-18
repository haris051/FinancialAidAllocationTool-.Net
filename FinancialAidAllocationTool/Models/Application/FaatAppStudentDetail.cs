using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppStudentDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        [Display(Name="Arid No")]
        public string AridNo { get; set; }

        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public double? Cpga { get; set; }
        [Display(Name="Email Address")]
        public string EmailAddress { get; set; }
        public string Residence { get; set; }
        [Display(Name="Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name="Reason To Apply")]
        public string ReasonToApply { get; set; }
        public string Semester {get;set;}
        public virtual FaatApplication Application { get; set; }
    }
}
