using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialAidAllocationTool.Models.Policy;
public class RuleOrderAttribute : ValidationAttribute
{

private IEnumerable list;

protected override ValidationResult IsValid(object value, ValidationContext validationContext)
{
    list = value as  IEnumerable;

     //= list.Where(o => o.value != null).ToList();

     //list.RemoveAll(item => item == null);
     var OrderedList1 = list.Cast<FaatRule>().Where(e => e != null).OrderByDescending(e=>e.Strength);
     var result1 = list.Cast<FaatRule>().Where(e => e != null).SequenceEqual(OrderedList1);
     var DuplicatesStrength = list.Cast<FaatRule>().Where(e => e != null).GroupBy(s => s.Strength)
							                             .Where(g => g.Count() > 1)
							                             .Select(g => g.Key).ToList();
     var result =true;
     if(DuplicatesStrength.Count>0)
     {
          result =false;
     }
     if(result1 && result)
     {
         return ValidationResult.Success;
     }
     else
     {
         return new ValidationResult("Rule must unique and Descending Order");
     }

     

}

}