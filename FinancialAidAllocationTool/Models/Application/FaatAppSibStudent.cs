using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 
using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Http;


namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatAppSibStudent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int SiblingId { get; set; }
        public string Name { get; set; }
        [Display(Name="Institute Name")]
        public string ClassInstituteName { get; set; }
        public string StdentCardFileName {get;set;}
        [NotMapped]    
        public IFormFile StudentCardFile {get;set;}
        public byte[] StdentCardFileData { get; set; }

        public string StdentCardFileType {get;set;}
        public virtual FaatAppSiblingInfo Sibling { get; set; }
    }
}
