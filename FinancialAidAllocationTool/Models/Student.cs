using System;
namespace FinancialAidAllocationTool.Models
{
    public class Student
    {
       public string RegisterNo {get;set;}
       public float CGPA {get;set;}
       public string Discipline{get;set;}
       public string Section {get;set;}
       public string Semester{get;set;}
        public string Name {get;set;}
        public string SemesterCount{get;set;}
       public double AllocationAmount{get;set;}
       public String Status {get;set;}
    }
}