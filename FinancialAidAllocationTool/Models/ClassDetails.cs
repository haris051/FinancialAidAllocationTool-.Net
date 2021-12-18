using System.Collections.Generic;
namespace FinancialAidAllocationTool.Models
{
 public class ClassDetails{
    
     public int ClassStrength{get;set;}
     public string Section {get;set;}
     public string SemesterCount {get;set;}
     public string Discipline {get;set;}
     public string SemesterNo{get;set;}
     
     public List<Student> StudentList  = new List<Student>();

     
     
 }
}