using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 
using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Http;


namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppSibJobHolder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int SiblingId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        [Display(Name="Monthly Income")]
        public double? MonthlyIncome { get; set; }
        public string ContractFileName {get;set;}

        public byte[] ContractFileData { get; set; }
       [NotMapped]     
       public IFormFile ContractFile {get;set;}
        public string ContractFileType {get;set;}
        public virtual FaatAppSiblingInfo Sibling { get; set; }
    }
}
