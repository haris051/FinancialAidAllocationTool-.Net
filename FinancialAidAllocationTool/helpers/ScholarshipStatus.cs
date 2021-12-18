using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialAidAllocationTool.Models;

public class ScholarshipStatusAttribute : ValidationAttribute
{
        private readonly FaaToolDBContext _context;
        private readonly String Type;

        public ScholarshipStatusAttribute(FaaToolDBContext context,String Type)
        {
              _context = context;
               this.Type = Type;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var otherProperty = validationContext.ObjectType.GetProperty(Type);
            var otherPropertyValue = (String)otherProperty.GetValue(validationContext.ObjectInstance, null);
           var PendingApplications = _context.FaatScholarLog.Where(e=>e.Status == "Pending" && e.Type==otherPropertyValue).Count();
            var validation = new ValidationResult("Please Complete the Pending Application before closing");

            if(PendingApplications > 0 && Type =="Need Based")
            {
               // ModelState.AddModelError("Error","Please Complete the Pending Application before closing");
              //  var validation = new ValidationResult("Please Complete the Pending Application before closing");
               // TempData["Error"] = "";
                return validation;
            }
            else if(PendingApplications > 0 && Type == "Merit Based")
            {
                return validation;
            }      
  
  
              return validation;
        }

}
