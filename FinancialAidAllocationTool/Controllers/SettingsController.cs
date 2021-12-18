using System;
using System.Linq;
using FinancialAidAllocationTool.Models;
using FinancialAidAllocationTool.Models.Settings;
using FinancialAidAllocationTool.Models.Policy;
using FinancialAidAllocationTool.Models.Ledger;
using FinancialAidAllocationTool.Models.IntakeSeason;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace FinancialAidAllocationTool.Controllers
{
    
    public class SettingsController : Controller
     {
         private readonly FaaToolDBContext _context;
         private readonly IConfiguration configuration;
         public SettingsController(IConfiguration configuration,FaaToolDBContext context)
        {
            this.configuration = configuration;
            _context = context;
        }
        public IActionResult index()
        {
            FaatPolicy policy = _context.FaatPolicy.Include(e=>e.FaatRule).ThenInclude(e=>e.FaatRuleDescription).Where(e=>e.IsSelected==1).FirstOrDefault();
            Settings settings = new Settings();
            settings.policy = policy;
            settings.DonationLedger = new DonationLedger();
            double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {
                TempData["DonationAccountVal"] = "Problem: Donation Account showing zero value";
                TempData["DonationAccountStatus"] = "Zero";
            }       
            else
            {
                TempData["DonationAccountVal"] = "Rs. " + result ;
                TempData["DonationAccountStatus"] = (result < 0 ? "Debit" :"Credit");
            }
            return View(settings);
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult index(Settings settings)
        {
          if(settings.DonationLedger.Credit!=0 || settings.DonationLedger.Credit!=null)
          {
          settings.DonationLedger.Debit=0;
          settings.DonationLedger.Memo="value of"+ settings.DonationLedger.Credit;
          settings.DonationLedger.TransactionDate = DateTime.Now;
          _context.Add(settings.DonationLedger);
          _context.SaveChanges();
          }
          
          var dbPolicy = _context.FaatPolicy.AsNoTracking().Include(e=>e.FaatRule).ThenInclude(e=>e.FaatRuleDescription).Where(e=>e.IsSelected==1).FirstOrDefault();
          bool flag1 =false;
          bool flag2 =false;
          if(dbPolicy.MeritMinCGPA!=settings.policy.MeritMinCGPA)
            {
               flag1 =true;       
          
            } 
             if(dbPolicy.NeedMinCGPA!=settings.policy.NeedMinCGPA)
            {
             //  flag1 =true;
               flag2 = true;       
          
            }
          for(int i=0 ;i<settings.policy.FaatRule.Count();i++)
          {
              if(dbPolicy.FaatRule.ToList()[i].Strength != settings.policy.FaatRule.ToList()[i].Strength)
              {
                  flag1 = true;
              }
              for(int j=0 ;j<settings.policy.FaatRule.ToList()[i].FaatRuleDescription.Count();j++)
              {
                  if(settings.policy.FaatRule.ToList()[i].FaatRuleDescription.ToList()[j].Amount!=dbPolicy.FaatRule.ToList()[i].FaatRuleDescription.ToList()[j].Amount)
                  {
                        flag1 = true;
                  }
              }
          }


          if(flag1 == true)
          {
          _context.Update(settings.policy);
          _context.SaveChanges();
          
          //perform shortlisting
          PerformMeritBasedShortListing ();
          }
          if(flag2== true)
          {

          _context.Update(settings.policy);
          _context.SaveChanges();
          PerformNeedBasedShortListing();
              
          }        
          return RedirectToAction(nameof(index));
        }

        void PerformMeritBasedShortListing ()
        {
            DBAccessLayer obj = new DBAccessLayer(configuration,_context);
            
            //Determine the Semeter for fetching Results
            List<FaatIntakeSeason> faatIntakeSeasonList = _context.FaatIntakeSeason.Include(a => a.FaatScholarshipStatus).ToList();
            // we had given reference in this place           
            FaatIntakeSeason faatIntakeSeason = null;            
            foreach(var i in faatIntakeSeasonList)
            {
                foreach( var j in i.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Merit Based")
                    {
                        faatIntakeSeason = i;             
                        break;
                    }
                }
            }

            String PreviousSemester = "";
            String CurrentSemester = "";
            if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake first) + Redirect to initial page
                TempData["error"] = "<script>alert('Please define intake season.');</script>";    
               
            }
            else
            {
                if(faatIntakeSeason.IntakeSeason == "FM")
                {
                    PreviousSemester = faatIntakeSeason.Year + "SM";
                    CurrentSemester = faatIntakeSeason.Year.ToString()+"FM";
                    //SM with Same year
                }   
                else if(faatIntakeSeason.IntakeSeason == "SM")
                {
                    //FM with Year - 1
                    PreviousSemester = (faatIntakeSeason.Year - 1) + "FM";
                    CurrentSemester = faatIntakeSeason.Year.ToString()+"SM";
                }
                else
                {
                    // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                    TempData["error"] = "<script>alert('Exception found while Defining Intake Season.');</script>";    
                    
                }
            }

            
            //Determine selected Policy
            var Policies = _context.FaatPolicy.Include(policy => policy.FaatRule)
             .ThenInclude(rule => rule.FaatRuleDescription).ToList();
             FaatPolicy SelectedPolicy = null;
             if(Policies != null)
             {
                SelectedPolicy = Policies.Find(s=>s.IsSelected==1);
             /*   string json = JsonConvert.SerializeObject(SelectedPolicy,new JsonSerializerSettings() {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });
           foreach( var j in faatIntakeSeason.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Merit Based")
                    {
                        j.Policy = json;
                        _context.Attach(j);
                        _context.Entry(j).Property("Policy").IsModified = true;
                        _context.SaveChanges();            
                        break;
                    }
                }
               */ 
             }
            //Fetching Results
            List<ClassDetails> StudentResultList = null;

            StudentResultList = obj.StudentResultList(CurrentSemester,PreviousSemester);
           // StudentCurrentList = obj.CurrentSemesterStudents(CurrentSemester);
         

            if(SelectedPolicy == null)
            {                
                TempData["error"] = "<script>alert('Please define/select policy.');</script>";    
               
            }

            //Prepare Scholar list or Perform Shortlisting
            List<ClassDetails> ScholarList = null;
            ScholarList = obj.PrepareScholarListModifiedVersion(StudentResultList, SelectedPolicy);
            
            //Insert Class Definition and Sholar list in DB
            List<FaatClassDefinition> ClassDefList = new List<FaatClassDefinition>();
            foreach(var classDetail in ScholarList)
            {
                FaatClassDefinition ClassDef = new FaatClassDefinition();
                ClassDef.Discipline = classDetail.Discipline;
                ClassDef.Semester = classDetail.SemesterNo;
                ClassDef.SemesterCount = classDetail.SemesterCount;
                ClassDef.Section = classDetail.Section;
                ClassDef.ClassStrength = classDetail.ClassStrength;
             
                ClassDef.FaatScholarLog = new List<FaatScholarLog>();    
                foreach(var student in classDetail.StudentList)
                {
                    FaatScholarLog Scholar = new FaatScholarLog();
                    Scholar.AridNo = student.RegisterNo;
                    Scholar.Name = student.Name;
                    Scholar.Cgpa = student.CGPA;
                    Scholar.AllocationAmount = student.AllocationAmount;
                    Scholar.DefaultAmount = student.AllocationAmount;
                    Scholar.Type = "Merit Based";
                    Scholar.Status = "Pending";
                    Scholar.InsertionTimestamp = DateTime.Now;
                    Scholar.UpdateTimestamp = DateTime.Now;

                  //  FaatScholarLedger ScholarLedger = new FaatScholarLedger();
                  //  ScholarLedger.TransactionDate = DateTime.Now;
                  //  ScholarLedger.Credit = student.AllocationAmount;
                  //  ScholarLedger.Memo = "Policy"; 

                    // we have done shallow copy here 

                   // Scholar.T = ScholarLedger;
                    ClassDef.FaatScholarLog.Add(Scholar);
                }

                ClassDefList.Add(ClassDef);
            }

            

            //Intially
            List<FaatClassDefinition> DelClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == CurrentSemester select c).ToList();
            
            if(DelClassDef.Count > 0)
            {                
                List<FaatScholarLog> MeritScholarList = new List<FaatScholarLog>();
                List<FaatScholarLog> NeedBasedScholarList = new List<FaatScholarLog>();

                foreach(var c in DelClassDef)
                {
                    foreach(var s in c.FaatScholarLog)
                    {
                        if(s.Type == "Merit Based")
                        {
                            MeritScholarList.Add(s);
                        }                    
                    }    
                }

                List<FaatScholarLedger> MeritRejectLedger = new List<FaatScholarLedger>();
                List<DonationLedger> MeritRejectDonationLedger = new List<DonationLedger>();
                foreach(var l in MeritScholarList)
                {
                    if(l.Status == "Accept")
                    {
                        MeritRejectLedger.Add(new FaatScholarLedger()
                        {
                            TransactionDate = DateTime.Now,
                            Debit = l.T.Credit,
                            Memo = "Reversal"                
                        });
                        MeritRejectDonationLedger.Add(new DonationLedger()
                        {
                            TransactionDate = DateTime.Now,
                            Credit = l.T.Credit,
                            Debit = 0,
                            Memo = "value of" + l.T.Credit 
                        }
                        
                        );

                    }
                }

                _context.AddRange(MeritRejectLedger);
                 _context.SaveChanges();
                _context.AddRange(MeritRejectDonationLedger);
                 _context.SaveChanges();
                _context.RemoveRange(MeritScholarList);              
                _context.SaveChanges();


                foreach(var cls in ClassDefList)
                {
                    try
                    {
                        FaatClassDefinition DBClass = _context.FaatClassDefinition.Where
                        (c => c.Discipline == cls.Discipline && c.Semester == cls.Semester &&
                        c.SemesterCount == cls.SemesterCount && c.Section == cls.Section).Include(e=>e.FaatScholarLog).FirstOrDefault();
                        
                        if(DBClass.FaatScholarLog != null)
                        {
                            cls.FaatScholarLog.ToList().Where(e=>e.Type=="Merit Based").ToList().ForEach(s => s.ClassId = DBClass.Id);
                            if(cls.FaatScholarLog != null && cls.FaatScholarLog.Count > 0)
                            {
                                _context.AddRange(cls.FaatScholarLog);
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                          //  string abc = "";
                           // abc= "M. Haris";  
                        }
                    }
                    catch
                    {                    
                     //   string abc = "";
                      //  abc= "M. Haris";         
                    }                    
                }                
            }
            else
            {
                _context.AddRange(ClassDefList);
                _context.SaveChanges();
            }
               
 //       
        }
        void PerformNeedBasedShortListing()
        {
              //Determine the Semeter for fetching Results
            List<FaatIntakeSeason> faatIntakeSeasonList = _context.FaatIntakeSeason.Include(a => a.FaatScholarshipStatus).ToList();
            // we had given reference in this place           
            FaatIntakeSeason faatIntakeSeason = null;            
            foreach(var i in faatIntakeSeasonList)
            {
                foreach( var j in i.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Need Based")
                    {
                        faatIntakeSeason = i;             
                        break;
                    }
                }
            }

            String PreviousSemester = "";
            String CurrentSemester = "";
            if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake first) + Redirect to initial page
                TempData["error"] = "<script>alert('Please define intake season.');</script>";    
               
            }
            else
            {
                if(faatIntakeSeason.IntakeSeason == "FM")
                {
                    PreviousSemester = faatIntakeSeason.Year + "SM";
                    CurrentSemester = faatIntakeSeason.Year.ToString()+"FM";
                    //SM with Same year
                }   
                else if(faatIntakeSeason.IntakeSeason == "SM")
                {
                    //FM with Year - 1
                    PreviousSemester = (faatIntakeSeason.Year - 1) + "FM";
                    CurrentSemester = faatIntakeSeason.Year.ToString()+"SM";
                }
                else
                {
                    // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                    TempData["error"] = "<script>alert('Exception found while Defining Intake Season.');</script>";    
                    
                }
            }

            
            //Determine selected Policy
            var Policies = _context.FaatPolicy.Include(policy => policy.FaatRule)
             .ThenInclude(rule => rule.FaatRuleDescription).ToList();
             FaatPolicy SelectedPolicy = null;
             if(Policies != null)
             {
                SelectedPolicy = Policies.Find(s=>s.IsSelected==1);
             
             /*   string json = JsonConvert.SerializeObject(SelectedPolicy,new JsonSerializerSettings() {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });
           foreach( var j in faatIntakeSeason.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Need Based")
                    {
                        j.Policy = json;
                        _context.Attach(j);
                        _context.Entry(j).Property("Policy").IsModified = true;
                        _context.SaveChanges();            
                        break;
                    }
                }
        */
             }
            var applications = _context.FaatApplication;
            List<FaatClassDefinition> classDefinitions = (from c in _context.FaatClassDefinition.Include(s => s.FaatApplications) where c.Semester == CurrentSemester  select c).ToList();
            List <FaatScholarLog> list = new List<FaatScholarLog>();
           foreach(var item in classDefinitions)
           {
                
                 foreach (var NeedBasedScholars in item.FaatApplications)
                 {   if(NeedBasedScholars.status=="Accept" || NeedBasedScholars.status=="Reject" || NeedBasedScholars.status=="Pending")

                 {
                     if(NeedBasedScholars.CGPA>=SelectedPolicy.NeedMinCGPA)
                     {
                         FaatScholarLog obj = new FaatScholarLog();
                         obj.Name = NeedBasedScholars.Name;
                         obj.Type = "Need Based";
                         obj.Status = "Pending";
                         obj.AridNo = NeedBasedScholars.AridNo;
                         obj.ClassId = item.Id;
                         obj.ApplicationId=NeedBasedScholars.Id;
                         obj.InsertionTimestamp = DateTime.Now;
                         obj.UpdateTimestamp = DateTime.Now;
                         obj.Cgpa = NeedBasedScholars.CGPA;
                         list.Add(obj);
                     }
                      
                 }
                    item.FaatApplications.Where(e=>e.status=="Accept" || e.status == "Reject" || e.status == "Pending").ToList().ForEach(e=>e.status="Pending");
                    _context.UpdateRange(item);
                    _context.SaveChanges();

                 }
           }
           List<FaatClassDefinition> DelClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog).ThenInclude(e=>e.T) where c.Semester == CurrentSemester   select c).ToList();
     
            if(DelClassDef.Count > 0)
            {                
                //List<FaatScholarLog> MeritScholarList = new List<FaatScholarLog>();
                List<FaatScholarLog> NeedBasedScholarList = new List<FaatScholarLog>();

                foreach(var c in DelClassDef)
                {
                    foreach(var s in c.FaatScholarLog)
                    {
                        if(s.Type == "Need Based")
                        {
                            NeedBasedScholarList.Add(s);
                        }                    
                    }    
                }

                List<FaatScholarLedger> NeedBasedRejectLedger = new List<FaatScholarLedger>();
                List<DonationLedger> NeedBasedRejectDonationLedger = new List<DonationLedger>();
                foreach(var l in NeedBasedScholarList)
                {
                    if(l.Status == "Accept")
                    {
                        NeedBasedRejectLedger.Add(new FaatScholarLedger()
                        {
                            TransactionDate = DateTime.Now,
                            Debit = l.T.Credit,
                            Memo = "Reversal"                
                        });
                        NeedBasedRejectDonationLedger.Add(new DonationLedger()
                        {
                            TransactionDate = DateTime.Now,
                            Credit = l.T.Credit,
                            Debit = 0,
                            Memo = "value of" + l.T.Credit 
                        }
                        
                        );

                    }
                }

                _context.AddRange(NeedBasedRejectLedger);
                 _context.SaveChanges();
                _context.AddRange(NeedBasedRejectDonationLedger);
                 _context.SaveChanges();
                _context.RemoveRange(NeedBasedScholarList);              
                _context.SaveChanges();
                _context.AddRange(list);
                _context.SaveChanges();
          /*
             foreach(var cls in classDefinitions)
                {
                    
                    try
                    {
                        FaatClassDefinition DBClass = _context.FaatClassDefinition.AsNoTracking().Where
                        (c => c.Discipline == cls.Discipline && c.Semester == cls.Semester &&
                        c.SemesterCount == cls.SemesterCount && c.Section == cls.Section).AsNoTracking().FirstOrDefault();
                        
                        if(DBClass!= null)
                        {
                            cls.FaatScholarLog.ToList().Where(e=>e.Type=="Need Based").ToList().ForEach(s => s.ClassId = DBClass.Id);
                            if(cls.FaatScholarLog != null)
                            {
                                _context.AddRange(cls.FaatScholarLog);
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                          //  string abc = "";
                           // abc= "M. Haris";  
                        }
                    }
                    catch
                    {                    
                     //   string abc = "";
                      //  abc= "M. Haris";         
                    }                    
                }
                */                
            }
            else
            {
                _context.AddRange(classDefinitions);
                _context.SaveChanges();
            }
               
 //        
          
        }

    }
}