using System;
using System.Collections.Generic;
using FinancialAidAllocationTool.Models.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 



namespace FinancialAidAllocationTool.Models
{
    public partial class Users 
    {
        public Users()
        {
            //FaatApplication = new HashSet<FaatApplication>();
            FaatAppComments = new HashSet<FaatAppComments>();
        }
        
        public string Name {get;set;}
        [Required]
        public  string  AridNo { get; set; }
        [Required]
        public  string Password { get; set; }
        public  string Role { get; set; }
        [Key]
        public  int Id { get; set; }

         public virtual ICollection<FaatAppComments> FaatAppComments { get; set; }

        //public virtual ICollection<FaatApplication> FaatApplication { get; set; }
    }
}
