using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace FinancialAidAllocationTool.Models.Application
{
        public partial class FaatAppComments
        {
            public FaatAppComments()
            {
                  
            }
            [Key]
            public int Id { get; set; }
           
            public int? UserId { get; set; }
            public int? Amount{get;set;}
            public int? ClassID {get;set;}
            public int? ApplicationId { get; set; }
            public string Comments { get; set; }
              [ForeignKey("ApplicationId")]
              public virtual FaatApplication Application { get; set; }
              [ForeignKey("UserId")]
           
              public virtual Users Users { get; set; }
              [ForeignKey("ClassID")]
              public virtual FaatClassDefinition ClassDefinition {get;set;}

        }
}