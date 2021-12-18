using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using Microsoft.AspNetCore.Http;


namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppResidenceInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Address { get; set; }
        [Display(Name="Phone No")]
        public string PhoneNo { get; set; }
        [Display(Name="Mode of Residence")]
        public string Mode { get; set; }

        public string ResidenceInfoFileName {get;set;}
       [NotMapped]    
        public IFormFile ResidenceFile{get;set;}
        public byte[] ResidenceInfoFileData { get; set; }
       // [NotMapped]
     //   public IFormFile RS {get;set;}
        public string ResidenceInfoFileType {get;set;}


        public virtual FaatApplication Application { get; set; }
    }
}
