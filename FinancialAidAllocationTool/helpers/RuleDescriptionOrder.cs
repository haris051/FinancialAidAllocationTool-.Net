using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialAidAllocationTool.Models.Policy;

public class RuleDescriptionOrderAttribute : ValidationAttribute
{
    private IEnumerable list;
    
  //  public string OtherProperty { get; set; }
   

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
       
        list = value as  IEnumerable;
        var OrderedList1 = list.Cast<FaatRuleDescription>().Where(e => e != null).OrderByDescending(e=>e.Amount);
        var result1 = list.Cast<FaatRuleDescription>().Where(e => e != null).SequenceEqual(OrderedList1);
        var OrderedList2  = list.Cast<FaatRuleDescription>().Where(e => e != null).OrderBy(e=>e.StudentNo);
        var result2 = list.Cast<FaatRuleDescription>().Where(e => e != null).SequenceEqual(OrderedList2);
        var DuplicatesStudent = list.Cast<FaatRuleDescription>().Where(e => e != null).GroupBy(s => s.StudentNo)
							                             .Where(g => g.Count() > 1)
							                             .Select(g => g.Key).ToList();
        var DuplicatesAmount  = list.Cast<FaatRuleDescription>().Where(e => e != null).GroupBy(e=>e.Amount)
                                                                .Where(g=>g.Count()>1)
                                                                .Select(g=>g.Key).ToList();

        var result3 = true;
        if(DuplicatesStudent.Count()>0)
        {
           result3 = false;
        }
        var result4 = true;
        if(DuplicatesAmount.Count()>0)
        {
            result4 = false;
        }
        if(result1 && result2 && result3 && result4)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Student and Amount must be uniqe and descending Order");
        }
        
    }
}