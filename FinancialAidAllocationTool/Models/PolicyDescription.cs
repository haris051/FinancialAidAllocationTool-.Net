using System.Collections.Generic;

namespace FinancialAidAllocationTool.Models
{
 public class PolicyDescription{
    
     public int ClassStrength{get;set;}
     public int Top {get;set;}    
     public List<int> Student_No{get;set;}
     public List<float> Amount{get;set;}

     public PolicyDescription()
     {
         Student_No = new List<int>();
         Amount = new List<float>();
     }
    public void AddStudent(int std)
    {
        Student_No.Add(std);
    }
     public void AddAmount(int amount)
    {
        Amount.Add(amount);
    }
 }
}