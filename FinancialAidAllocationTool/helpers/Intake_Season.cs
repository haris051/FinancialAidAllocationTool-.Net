using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinancialAidAllocationTool.Models;

public class Intake_SeasonAttribute : ValidationAttribute
{
    private String Season;
    private readonly String Year;
  //  private readonly FaaToolDBContext _context;
  //  public string OtherProperty { get; set; }
    public Intake_SeasonAttribute(String Season)
    {
        this.Season = Season;
        
       
    }
     /*
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {   
       
        var _context = (FaaToolDBContext)validationContext
                         .GetService(typeof(FaaToolDBContext));
        Season = value as String;
        var otherProperty = validationContext.ObjectType.GetProperty(Year);
        var otherPropertyValue = (int)otherProperty.GetValue(validationContext.ObjectInstance, null);
        var Year_Db = _context.FaatIntakeSeason.ToList().OrderByDescending(e=>e.InsertionTimestamp).Select(e=>e.Year).FirstOrDefault();
        if(int.Parse(Year_Db.ToString())< otherPropertyValue)
        {
             return ValidationResult.Success;
        }
             
             return ValidationResult.Success;
             
    }
    */
}