using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialAidAllocationTool.Models.Policy;

public class RuleDescriptionAttribute : ValidationAttribute
{
    private readonly String minElements;
    private IEnumerable list;
  //  public string OtherProperty { get; set; }
    public RuleDescriptionAttribute(String minElements)
    {
        this.minElements = minElements;
       
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(minElements);
        var otherPropertyValue = (int)otherProperty.GetValue(validationContext.ObjectInstance, null);
        var result = false;
        var result1 = false;
        var validation = new ValidationResult("Top and Rule Description must be same");
        var validation1 = new ValidationResult("Student No must be Less than or Equal to Top");
        var validation2 = new ValidationResult("Top and Rule Description must be same and Student No must be Less than or Equal to Top");
        list = value as  IEnumerable;
        if(list.Cast<object>().Where(e => e != null).Count() == otherPropertyValue)
        {
            result = true;
        }
        
        if(list.Cast<FaatRuleDescription>().Where(e=>e != null && e.StudentNo <= otherPropertyValue).Count()>0)
        {
            result1 = true;
        }
        if(result==false && result1==true)
        {
            return validation;
        }
        if(result1==false && result ==true)
        {
            return validation1;
        }
        if(result==true && result1==true)
        {
            return ValidationResult.Success;
        }
        else
        {
            return validation2;
        }
        
    }
}