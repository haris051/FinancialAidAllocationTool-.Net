using System;
using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using Microsoft.AspNetCore.Http;




namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppParentDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        [Required(ErrorMessage="Please enter Guardian name.")]
        [Display(Name="Name")]
        public string GName { get; set; }
        [Display(Name="Company")]

        public string GCompany { get; set; }
        [Display(Name="Occupation")]

        public string GOccupation { get; set; }
        [Display(Name="Designation")]

        public string GDesignation { get; set; }
        [Display(Name="Monthly Income")]

        public double? GMonthlyIncome { get; set; }
        [Display(Name="Office No")]

        public string GOfficeTelNo { get; set; }
        [Display(Name="Office Address")]

        public string GOfficeAddress { get; set; }
        [Display(Name="Email Address")]

        public string GEmailAddress { get; set; }
        [Display(Name="Name")]
        public string MName { get; set; }
        [Display(Name="Company")]

        public string MCompany { get; set; }
        [Display(Name="Occupation")]
        public string MOccupation { get; set; }
         [Display(Name="Designation")]
        public string MDesignation { get; set; }
        [Display(Name="Monthly Income")]
        public double? MMonthlyIncome { get; set; }
        [Display(Name="Office No")]
        public string MOfficeTelNo { get; set; }
         [Display(Name="Office Address")]
        public string MOfficeAddress { get; set; }
        [Display(Name="Email Address")]
        public string MEmailAddress { get; set; }
        [Display(Name="Financing Person")]
        public string FinancingPerson { get; set; }
        [Display(Name="Father is Alive")]
        public bool FatherIsAlive { get; set; }
        [Display(Name="Mother is Alive")]
        public bool MotherIsAlive { get; set; }

        public string FatherCNICDeathCertificateFileName {get;set;}
        [NotMapped] 
        public IFormFile FatherFile{get;set;}
         [NotMapped] 
         public IFormFile MotherFile{get;set;}
        public byte[] FatherCNICDeathCertificateFileData { get; set; }

        public string FatherCNICDeathCertificateFileType {get;set;}
        public string MotherCNICDeathCertificateFileName {get;set;}
      
        public byte[] MotherCNICDeathCertificateFileData { get; set; }
        
        public string MotherCNICDeathCertificateFileType {get;set;}
        public virtual ICollection<FaatAppGuardianOtherIncomeResourceFiles> FaatAppGuardianOtherIncomeResourceFiles { get; set; }
        public virtual ICollection<FaatAppMotherOtherIncomeResourceFiles> FaatAppMotherOtherIncomeResourceFiles { get; set; }
        
        public virtual FaatApplication Application { get; set; }
    }
}
