using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatFiles
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }

        public string FileName {get;set;}

        public byte[] FileData { get; set; }

        public string FileType {get;set;}
        public virtual FaatApplication Application { get; set; }
    }

}