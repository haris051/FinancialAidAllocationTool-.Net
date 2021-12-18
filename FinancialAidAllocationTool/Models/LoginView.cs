 using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Mvc;
 namespace FinancialAidAllocationTool.Models
{
    public class LoginView {
 
       
        [Required]
        //[EmailAddress]
                public string Email { get; set; }

        
        [DataType(DataType.Password)]
        public string Password { get; set; }

         public string Role {get;set;}

         [Display(Name = "Remember me")]
         public bool RememberMe { get; set; }   
          
         public string Mode { get;set;}
    }

}