using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 

namespace FinancialAidAllocationTool.Models.Application
{
    public partial class FaatApplication
    {
        public FaatApplication()
        {
            FaatAppParentDetail = new HashSet<FaatAppParentDetail>();
            FaatAppResidenceInfo = new HashSet<FaatAppResidenceInfo>();
            FaatAppSiblingInfo = new HashSet<FaatAppSiblingInfo>();
            FaatAppStudentDetail = new HashSet<FaatAppStudentDetail>();

            FaatScholarLog = new HashSet<FaatScholarLog>();
            FaatFiles = new HashSet<FaatFiles>();
            FaatAppComments = new HashSet<FaatAppComments>();
            
       //     Users = new HashSet<Users>();
           // FaatAppStudentDetail.Add(new FaatAppStudentDetail());
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public int? ClassId {get;set;}
        //public int? UserId { get; set; }
        //public string Status { get; set; }
        public DateTime InsertionTimestamp { get; set; }
        public string AridNo { get; set; }
        public string Name { get; set; }
        public Double? CGPA {get;set;}
        public String status{get;set;}
        [NotMapped]   
        public IFormFile file {get;set;} 
        public byte[] UserImage {get;set;}

        public String UserImageFileType {get;set;}

        public String UserImageFileName {get;set;}
        //public string Class { get; set; }
        //public string Section { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public byte[] ApplicationData { get; set; }

    
        //public virtual Users User { get; set; }
        public virtual ICollection<FaatAppParentDetail> FaatAppParentDetail { get; set; }
        public virtual ICollection<FaatAppResidenceInfo> FaatAppResidenceInfo { get; set; }
        public virtual ICollection<FaatAppSiblingInfo> FaatAppSiblingInfo { get; set; }
        public virtual ICollection<FaatAppStudentDetail> FaatAppStudentDetail { get; set; }

        public virtual ICollection<FaatScholarLog> FaatScholarLog { get; set; }
        public virtual ICollection<FaatFiles> FaatFiles { get; set; }
        public virtual ICollection<FaatAppComments> FaatAppComments { get; set; }
        public virtual FaatClassDefinition FaatClassDefinition {get;set;}

   //      public virtual ICollection<Users> Users { get; set; }

    }
}
