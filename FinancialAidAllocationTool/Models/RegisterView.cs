 using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Mvc;
 
 namespace FinancialAidAllocationTool.Models
{
    public class RegisterView {
 
       
        [Required]
        //[EmailAddress]
        [Remote(action:"EmailAvailabilty",controller: "Account")]
        public string AridNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
        ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

}