using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using FinancialAidAllocationTool.Models.Application;
 using Microsoft.AspNetCore.Http;

namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppGuardianOtherIncomeResourceFiles
    {
        public int Id { get; set; }
        public string ResourceType {get;set;}
        public int? Income {get;set;}
        public int FaatAppParentDetailId { get; set; }

        public string FileName {get;set;}
        [NotMapped]   
        public IFormFile file {get;set;}  
         public byte[] FileData { get; set; }

        public string FileType {get;set;}
        public virtual FaatAppParentDetail FaatAppParentDetail { get; set; }
    }

}