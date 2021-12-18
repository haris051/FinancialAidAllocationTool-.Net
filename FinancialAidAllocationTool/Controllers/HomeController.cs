using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancialAidAllocationTool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using FinancialAidAllocationTool.Models.Application;
using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models.IntakeSeason;
using FinancialAidAllocationTool.Models.Ledger;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using FinancialAidAllocationTool.Models.Policy;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text.Json;
namespace FinancialAidAllocationTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public static float[] AllocationAmount;
        public static string[] RegistrationNo;
        

         private readonly FaaToolDBContext _context;

       
            
        

        public HomeController(IConfiguration configuration,FaaToolDBContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

      
        public IActionResult Index()
        {
         //   ViewData["Roles"] = TempData["Roles"];
            //TempData["error"] = "<script>alert('Fall semister is already complete.');</script>";
            //TempData["error"] = "";
            
            return RedirectToAction("Dashboard");
        } 

      
         
           
        public IActionResult DashBoard()
        {
            DBAccessLayer obj = new DBAccessLayer(configuration,_context);
            obj.SeasonDefinition();
            double result = 0; 
            double.TryParse(_context.DonationLedger.AsNoTracking().Sum(c => c.Credit - c.Debit).ToString(), out result);

            TempData["Budget"] = result;
             double? MeritCgpa = _context.FaatPolicy.AsNoTracking().Where(e=>e.IsSelected==1).Select(e=>e.MeritMinCGPA).FirstOrDefault();
             TempData["MeritCgpa"] = MeritCgpa;
            
            
            
            double? NeedCgpa = _context.FaatPolicy.AsNoTracking().Where(e=>e.IsSelected==1).Select(e=>e.NeedMinCGPA).FirstOrDefault();
            TempData["NeedCgpa"]  = NeedCgpa;
             FaatIntakeSeason faatIntakeSeason = null;         
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.AsNoTracking().Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
            if(PendingNeedBasedAppStatus != null)
            {
            faatIntakeSeason = _context.FaatIntakeSeason.AsNoTracking().Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            }
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                if(User.IsInRole("Admin"))
                {
                    TempData["Error"] = "Current Semester Need Base Scholarship is Closed. Define New Intake Season or Modify the Current Need Based Scholarship from Manage Scholarship Process";
                }
                if(User.IsInRole("Committee"))
                {
                    TempData["Error"] = "Sorry Current Semester ScholarShip is Closed";
                }
                return View();
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                
            }
            var classes = _context.FaatClassDefinition.AsNoTracking().Include(e=>e.FaatScholarLog).Where(e=>e.Semester==Semester);
            var MeritAcceptCount = 0;
            var MeritRejectCount = 0;
            var NeedAcceptCount =0;
            var NeedRejectCount =0;
            foreach(var item in classes)
            {
                MeritAcceptCount = MeritAcceptCount + item.FaatScholarLog.Where(e=>e.Status=="Accept" && e.Type=="Merit Based").Count();
                MeritRejectCount = MeritRejectCount + item.FaatScholarLog.Where(e=>e.Status=="Reject" && e.Type=="Merit Based").Count();
                NeedAcceptCount = NeedAcceptCount + item.FaatScholarLog.Where(e=>e.Status=="Accept" && e.Type == "Need Based").Count();
                NeedRejectCount = NeedRejectCount + item.FaatScholarLog.Where(e=>e.Status=="Reject" && e.Type=="Need Based").Count();

            }
            TempData["MeritAccept"] = MeritAcceptCount;
            TempData["NeedAccept"] = NeedAcceptCount;
            TempData["MeritReject"] = MeritRejectCount;
            TempData["NeedReject"] = NeedRejectCount;

            TempData["Student"] = _context.Users.AsNoTracking().Where(e=>e.Role=="Student").Count();
            TempData["Committee"] = _context.Users.AsNoTracking().Where(e=>e.Role=="Committee").Count();

            return View();
        }

        [Authorize(Roles="Admin,Committee")]
        public ActionResult NeedBasedList(string Discipline)
        {     
            FaatIntakeSeason faatIntakeSeason = null;         
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
            if(PendingNeedBasedAppStatus != null)
            {
            faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            }
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                if(User.IsInRole("Admin"))
                {
                    TempData["Error"] = "Current Semester Need Base Scholarship is Closed. Define New Intake Season or Modify the Current Need Based Scholarship from Manage Scholarship Process";
                }
                if(User.IsInRole("Committee"))
                {
                    TempData["Error"] = "Sorry Current Semester ScholarShip is Closed";
                }
                return View();
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T).Include(e=>e.FaatScholarLog).ThenInclude(e=>e.A).Include(e=>e.FaatApplications) where c.Semester == Semester && c.Discipline == Discipline select c).ToList();

            List<FaatClassDefinition> ClassDef = null;
             foreach(var item in SelClassDef)
             {
                 item.FaatScholarLog = item.FaatScholarLog.Where(e=>e.Type == "Need Based").ToList();
                 item.FaatApplications  = item.FaatApplications.Where(e=>e.status == "Pending" || e.status == "Accept" || e.status== "Reject").ToList();
             }

            ClassDef = SelClassDef.Where(c => c.FaatScholarLog.Any( s => s.Type == "Need Based") || c.FaatApplications.Where(e=>e.status == "Pending" || e.status == "Accept" || e.status== "Reject").Count()>0).ToList();
            

            foreach(var c in ClassDef)
            {
                c.FaatScholarLog = c.FaatScholarLog.OrderByDescending(s => s.Cgpa).ToList();
       
            }

            ClassDef = ClassDef.OrderByDescending(c => c.ClassStrength).ToList();
            

            
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
     //       var ApplicationID = _context.FaatScholarLog.Where(e=>e.CD.Semester==Semester).Select(e=>e.ApplicationId);
      //      var Applications = _context.FaatApplication.Include(e=>e.FaatAppStudentDetail).Include(e=>e.FaatAppParentDetail).Include(e=>e.FaatAppResidenceInfo).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibJobHolder).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibStudent).Include(e=>e.FaatFiles).Include(e=>e.FaatAppComments).Where(e=>e.FaatAppStudentDetail.FirstOrDefault().Semester==Semester).ToList();
       //     var NeedBasedData = new Tuple<List<FaatClassDefinition>,List<FaatApplication>>(ClassDef,Applications);
            return View(ClassDef);
        }
        [Authorize(Roles="Admin")]

        public IActionResult MeritBasedList(string Discipline)
            {    
                FaatIntakeSeason faatIntakeSeason = null;          
            FaatScholarshipStatus PendingMeritBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
            if(PendingMeritBasedAppStatus != null)
            {
            faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingMeritBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            }
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                if(User.IsInRole("Admin"))
                {
                    TempData["Error"] = "Current Semester Merit Base Scholarship is Closed. Define New Intake Season or Modify the Current Merit Based Scholarship from Manage Scholarship Process";
                }
                if(User.IsInRole("Committee"))
                {
                    TempData["Error"] = "Sorry Current Semester ScholarShip is Closed";
                }
                return View();
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
                 Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;
               /* if(faatIntakeSeason.IntakeSeason == "FM")
                {
                    Semester = faatIntakeSeason.Year + "SM";
                    //SM with Same year
                }   
                else if(faatIntakeSeason.IntakeSeason == "SM")
                {
                    //FM with Year - 1
                    Semester = (faatIntakeSeason.Year - 1) + "FM";
                }
                else
                {
                    // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                }
                */
            }
            
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester && c.Discipline == Discipline select c).ToList();
             foreach(var item in SelClassDef)
             {
                 item.FaatScholarLog = item.FaatScholarLog.Where(e=>e.Type == "Merit Based" || e.IsManual == true).ToList();
             }
            List<FaatClassDefinition> ClassDef = SelClassDef.Where(c => c.FaatScholarLog.Any( s => s.Type == "Merit Based")).ToList();
            //List<FaatClassDefinition> ClassDef = new List<FaatClassDefinition>();
            //List<FaatClassDefinition> ClassDef = _context.FaatClassDefinition.Include(c => c.FaatScholarLog).Where( c => c.FaatScholarLog.Any( s => s.Type == "Need Based" && s.Status == "Pending")).ToList();
            //List<FaatClassDefinition> ClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog.Any( s => s.Type == "Need Based" && s.Status == "Pending")).ThenInclude(ss => ss.T) where c.Semester == Semester && c.Discipline == Discipline select c).ToList();
            /*foreach(var c in SelClassDef)
            {                
                FaatClassDefinition Cldef = new FaatClassDefinition();
                Cldef.FaatScholarLog = new List<FaatScholarLog>();

                Cldef = c;                
                foreach(var s in c.FaatScholarLog)
                {                    
                    if(s.Type == "Merit Based")
                    {
                        Cldef.FaatScholarLog.Add(s);
                    }
                }

                ClassDef.Add(Cldef);    
            }*/

            foreach(var c in ClassDef)
            {
                c.FaatScholarLog = c.FaatScholarLog.OrderByDescending(s => s.Cgpa).ToList();
       
            }

            ClassDef = ClassDef.OrderByDescending(c => c.ClassStrength).ToList();
            
            int RejectCount = 0;
            int AcceptCount = 0;
            int TotalCount = 0;
            foreach(var c in ClassDef)
            {
                foreach(var s in c.FaatScholarLog)
                {
                    if(s.Type == "Merit Based" && s.Status == "Reject")
                    {
                        RejectCount++;
                        TotalCount++;
                    }
                    if(s.Type == "Merit Based" && s.Status == "Accept")
                    {
                        AcceptCount++;
                        TotalCount++;    
                    }
                }
            }

            int IsAcceptAllEnabled = 0;
            int IsRejectAllEnabled = 0;

            if(RejectCount == 0)
            {
                //Disable Reject All;
                IsRejectAllEnabled = 0;
            }
            else
            {
                //Enable Reject All;
                IsRejectAllEnabled = 1;
            }

            if(AcceptCount == 0)
            {
                //Disable Accept All;
                IsAcceptAllEnabled = 0;
            }
            else
            {
                //Enable Accept All;
                IsAcceptAllEnabled = 1;
            }

            TempData["IsAcceptAllEnabled"] = IsAcceptAllEnabled.ToString();
            TempData["IsRejectAllEnabled"] = IsRejectAllEnabled.ToString();

            
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
            
            return View(ClassDef);
        }

        [Authorize(Roles="Admin")]
        public IActionResult MeritList(string Discipline)
        {              
            FaatScholarshipStatus PendingScholarshipStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingScholarshipStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
                if(faatIntakeSeason.IntakeSeason == "FM")
                {
                    Semester = faatIntakeSeason.Year + "SM";
                    //SM with Same year
                }   
                else if(faatIntakeSeason.IntakeSeason == "SM")
                {
                    //FM with Year - 1
                    Semester = (faatIntakeSeason.Year - 1) + "FM";
                }
                else
                {
                    // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                }
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester && c.Discipline == Discipline select c).ToList();

            List<FaatClassDefinition> ClassDef = new List<FaatClassDefinition>();
            foreach(var c in SelClassDef)
            {                
                FaatClassDefinition Cldef = new FaatClassDefinition();
                Cldef.FaatScholarLog = new List<FaatScholarLog>();

                Cldef = c;                
                foreach(var s in c.FaatScholarLog)
                {                    
                    if(s.Type == "Merit Based")
                    {
                        Cldef.FaatScholarLog.Add(s);
                    }
                }

                ClassDef.Add(Cldef);    
            }

            foreach(var c in ClassDef)
            {
                c.FaatScholarLog = c.FaatScholarLog.OrderByDescending(s => s.Cgpa).ToList();
            }

            ClassDef = ClassDef.OrderByDescending(c => c.ClassStrength).ToList();
            
            int RejectCount = 0;
            int AcceptCount = 0;
            int TotalCount = 0;
            foreach(var c in ClassDef)
            {
                foreach(var s in c.FaatScholarLog)
                {
                    if(s.Type == "Merit Based" && s.Status == "Reject")
                    {
                        RejectCount++;
                        TotalCount++;
                    }
                    if(s.Type == "Merit Based" && s.Status == "Accept")
                    {
                        AcceptCount++;
                        TotalCount++;    
                    }
                }
            }

            int IsAcceptAllEnabled = 0;
            int IsRejectAllEnabled = 0;

            if(RejectCount == 0)
            {
                //Disable Reject All;
                IsRejectAllEnabled = 0;
            }
            else
            {
                //Enable Reject All;
                IsRejectAllEnabled = 1;
            }

            if(AcceptCount == 0)
            {
                //Disable Accept All;
                IsAcceptAllEnabled = 0;
            }
            else
            {
                //Enable Accept All;
                IsAcceptAllEnabled = 1;
            }

            TempData["IsAcceptAllEnabled"] = IsAcceptAllEnabled.ToString();
            TempData["IsRejectAllEnabled"] = IsRejectAllEnabled.ToString();

            return View(ClassDef);
        }

        // [Authorize(Roles="Admin")]
        // public IActionResult MeritList(string Discipline, string old)
        // {            
        //     List<ClassDetails> StudentResultList = null;
        //     List<ClassDetails> ScholarList = null;
        //     DBAccessLayer obj = new DBAccessLayer(configuration,_context);
        //     var Policies = _context.FaatPolicy.Include(policy => policy.FaatRule)
        //      .ThenInclude(rule => rule.FaatRuleDescription).ToList();;
          
        //     StudentResultList = obj.StudentResultList(Discipline);
            
        //     ScholarList = obj.PrepareScholarList(StudentResultList,Policies.Find(s=>s.IsSelected==1));
        //     var StudentLog = _context.FaatScholarLog.ToList();

        //     if(RegistrationNo!=null)
        //     {
        //        foreach(var Scholar in ScholarList)
        //        {
        //             foreach(var StudentDetail in Scholar.StudentList)
        //             {
        //                 for(int i =0 ;i<RegistrationNo.Count();i++)
        //                 {
        //                     if(StudentDetail.RegisterNo==RegistrationNo[i])
        //                     {
        //                         StudentDetail.AllocationAmount=AllocationAmount[i];
        //                     }
        //                 }
        //             }
 
        //         }
        //     }
            
        //     return View(ScholarList);
        // }

        [HttpPost]
        public IActionResult Settings(float Budget,float Allocation)
        {
            // DBAccessLayer obj = new DBAccessLayer(configuration);
            // obj.B  settings.Add( new Settings());
            return View();
        }
        /*
        [HttpPost]
        public IActionResult MeritList(string Discipline,String SemesterNo,
        String RegistrationNo,float AllocationAmount,String Submit,String Status,String SemesterCount,String Section)
        {
            //IEnumerable  settings.Add( new Settings());
            List<ClassDetails> ScholarList = null;
            List<ClassDetails> list = new List<ClassDetails>();
            

            DBAccessLayer obj = new DBAccessLayer(configuration,_context);
           
             obj.StudentLog(SemesterNo,RegistrationNo,Submit,AllocationAmount,Discipline,SemesterCount,Section,"MeritList");
             var Policies = _context.FaatPolicy.Include(policy => policy.FaatRule)
             .ThenInclude(rule => rule.FaatRuleDescription).ToList();

         //   List<Settings> SettingsList = obj.GetSettings();
         //   SettingsList.Add( new Models.Settings());
           // SettingsList.Add( new Settings());

         //   Settings SelectedSettings = SettingsList.Find(s => s.IsSelected == true);

            //  
            list = obj.StudentResultList(Discipline);  
            ScholarList = obj.PrepareScholarList(list, Policies.Find(s => s.IsSelected == 1));
            
            foreach(var Scholars in ScholarList)
            {
                   foreach(var ScholarDetails in Scholars.StudentList)
                   {
                       if(ScholarDetails.RegisterNo == RegistrationNo)
                       {
                           ScholarDetails.AllocationAmount=AllocationAmount;
                       }
                   }

            }
            //list = obj.MeritList(Discipline, SelectedSettings ).ToList();
            
            //settings.Add( new Settings());

            return View(ScholarList);
        }
        */
        [HttpPost]
        public IActionResult CompleteList(float[] AllocatedAmount,string[]  RegistrationNo, string Status, string Type,string Discipline)
        {
            DBAccessLayer obj = new DBAccessLayer(configuration,_context);
            
            String Semester = "2019FM";
            obj.CompleteList( RegistrationNo, Semester,AllocatedAmount,Status,Type);
            HomeController.RegistrationNo = RegistrationNo;
            HomeController.AllocationAmount = AllocatedAmount;
            return RedirectToAction ("MeritList",new{Discipline=Discipline});
        }

         [Authorize(Roles="Admin")]
         [HttpGet]
        public IActionResult NeedBasedApplications()
        {
            return View(_context.FaatScholarLog.ToList());
            //return View(_context.FaatApplication.ToList());
        }

        [HttpPost]
        public IActionResult NeedBasedApplications(String status)
        {
            return View(_context.FaatScholarLog.ToList());
            //return View (_context.FaatApplication.ToList());
        }
        
        // public async Task<IActionResult> Status(string ID,string Status)
        // {
        //         FaatApplication obj = new FaatApplication();
        //         obj = _context.FaatApplication.ToList().FirstOrDefault(d=>d.Id==int.Parse(ID));
        //         obj.Status=Status;
        //         _context.Update(obj);
        //         await _context.SaveChangesAsync();
        //         return Ok();
        // }

        public bool AcceptAllNeedBasedAny(String Arid_No, double? AllocationAmount,
            List<FaatClassDefinition> SemesterClassDef)
        {               
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SemesterClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Need Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    FaatApplication app = new FaatApplication();
                      app.status = "Accept";
                      app.ClassId = s.ClassId;
                      app.Id = s.ApplicationId;
                      _context.FaatApplication.Attach(app);
                      
                     _context.Entry(app).Property(x => x.status).IsModified = true;
                     _context.SaveChanges();
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    /*Check Ledger Entry and Maintain Ledger*/
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();           
                    }
                    else
                    {
                        /*Reject Found.*/
                    }
                                        
                    FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           

            
                    FaatScholarLog scholar = new FaatScholarLog()
                    {
                        Id = StdClassDef.FaatScholarLog.ToList()[0].Id,
                        ApplicationId = StdClassDef.FaatScholarLog.ToList()[0].ApplicationId,
                        AridNo = StdClassDef.FaatScholarLog.ToList()[0].AridNo,
                        Name = StdClassDef.FaatScholarLog.ToList()[0].Name,
                        ClassId = StdClassDef.FaatScholarLog.ToList()[0].ClassId,
                        Status = "Accept",
                        Type = StdClassDef.FaatScholarLog.ToList()[0].Type,
                        Cgpa = StdClassDef.FaatScholarLog.ToList()[0].Cgpa,
                        AllocationAmount  = AllocationAmount,
                        DefaultAmount = 0,
                        InsertionTimestamp = StdClassDef.FaatScholarLog.ToList()[0].InsertionTimestamp,
                        UpdateTimestamp = DateTime.Now
                    };
                    
                    scholar.T = null;         
                    scholar.Tid = TAcceptSchLedger.TransactionId;
                    
                    _context.Update(scholar);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;                                               
        }

        [HttpPost]
        public ActionResult AcceptAllNeedBased(List<FaatClassDefinition> faatClassDefinition)
        {
            Boolean isAmountValid = !faatClassDefinition.Any(c => c.FaatScholarLog.Any(s => s.AllocationAmount == null || s.AllocationAmount == 0 ));
            if(!isAmountValid)
            {
                TempData["Error"] = "Please give valid amount!";
            }
            else
            {
                
                /*Retrieving Semester*/
                FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
                FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
                
                String Semester = "";
                if(faatIntakeSeason == null)
                {                    
                    TempData["Error"] = "Define Intake and prepare scholarship list first.";
                }
                else
                {
                    Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                

                    List<FaatClassDefinition> SemesterClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();

                    bool IsSuccess = true;
                    foreach(var cl in faatClassDefinition)
                    {
                        foreach(var std in cl.FaatScholarLog)
                        {
                            bool result = AcceptAllNeedBasedAny(std.AridNo, std.AllocationAmount, SemesterClassDef);
                            if(result == false)
                            {
                                IsSuccess = false;
                            }
                        }
                    }
                    if(IsSuccess == false)
                    {
                        TempData["Error"] = "Accept All was not successfully executed for all the students.";                    
                    }
                }
            }
            
            return RedirectToAction (nameof(NeedBasedList),new{Discipline=faatClassDefinition[0].Discipline});
        }


        public void RejectAllNeedBasedAny(String Arid_No, double? AllocationAmount, 
            List<FaatClassDefinition> SemisterClassDef)
        {                     
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SemisterClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Need Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    FaatApplication app = new FaatApplication();
                      app.status = "Reject";
                      app.ClassId = s.ClassId;
                      app.Id = s.ApplicationId;
                      _context.FaatApplication.Attach(app);
                      
                     _context.Entry(app).Property(x => x.status).IsModified = true;
                     _context.SaveChanges();
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    //Check Ledger Entry and Maintain Ledger
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {                        
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = AllocationAmount;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        //StdClassDef.FaatScholarLog.ToList()[0].T = TRejectSchLedger;
                        StdClassDef.FaatScholarLog.ToList()[0].Tid = TRejectSchLedger.TransactionId;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Tid).IsModified = true;
            
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Reject Found.
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = AllocationAmount;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        
                        _context.SaveChanges();
                    }                                        

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw e;
                    //return false;
                }
            }            
            //return true;
        }

        [HttpPost]
        public ActionResult RejectAllNeedBased(List<FaatClassDefinition> faatClassDefinition)
        {
            //Retrieving Semister
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                TempData["Error"] = "Define Intake and prepare scholarship list first.";
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                
                List<FaatClassDefinition> SemisterClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();

                bool IsSuccess = true;
                foreach(var cl in faatClassDefinition)
                {
                    foreach(var std in cl.FaatScholarLog)
                    {
                        try
                        {
                            std.AllocationAmount = std.AllocationAmount == null ? 0 : std.AllocationAmount; 
                            RejectAllNeedBasedAny(std.AridNo, std.AllocationAmount, SemisterClassDef);
                        }
                        catch(Exception e)
                        {
                            IsSuccess = false; 
                            continue;
                        }
                    }
                }

                if(IsSuccess == false)
                {
                    TempData["Error"] = "Reject All was not successfully executed for all the students.";           
                }
            }           
            
            return RedirectToAction (nameof(NeedBasedList),new{Discipline=faatClassDefinition[0].Discipline});
        }
       
        [HttpPost]
        public ActionResult AcceptNeedBased(String Arid_No, double AllocationAmount)
        {         
            int? Tid = 0;
            /*Retrieving Discipline*/
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                /* Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page*/
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();
                        
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SelClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Need Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                   FaatApplication app = new FaatApplication();
                      app.status = "Accept";
                      app.ClassId = s.ClassId;
                      app.Id = s.ApplicationId;
                      _context.FaatApplication.Attach(app);
                      
                     _context.Entry(app).Property(x => x.status).IsModified = true;
                     _context.SaveChanges();
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,                            
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                       
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                         Tid = s.Tid;
                    break;        
                }
            }

            if(AllocationAmount == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "",
                    DonationAccountStatus =  "",
                    status ="Error",
                    Message = "Please give valid amount!" 
                });
            }
            

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    /*Check Ledger Entry and Maintain Ledger*/
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();           
                    }
                    else
                    {
                        /*Reject Found.*/
                    }
                                        
                    FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           

            
                    FaatScholarLog scholar = new FaatScholarLog()
                    {
                        Id = StdClassDef.FaatScholarLog.ToList()[0].Id,
                        ApplicationId = StdClassDef.FaatScholarLog.ToList()[0].ApplicationId,
                        AridNo = StdClassDef.FaatScholarLog.ToList()[0].AridNo,
                        Name = StdClassDef.FaatScholarLog.ToList()[0].Name,
                        ClassId = StdClassDef.FaatScholarLog.ToList()[0].ClassId,
                        Status = "Accept",
                        Type = StdClassDef.FaatScholarLog.ToList()[0].Type,
                        Cgpa = StdClassDef.FaatScholarLog.ToList()[0].Cgpa,
                        AllocationAmount  = AllocationAmount,
                        DefaultAmount = 0,
                        InsertionTimestamp = StdClassDef.FaatScholarLog.ToList()[0].InsertionTimestamp,
                        UpdateTimestamp = DateTime.Now
                    };
                    
                    scholar.T = null;         
                    scholar.Tid = TAcceptSchLedger.TransactionId;
                    Tid = TAcceptSchLedger.TransactionId;
                    _context.Update(scholar);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
            }                       

            double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= Tid
                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid = Tid
                });
            }            
        }

        [HttpPost]
        public ActionResult RejectNeedBased(String Arid_No, double? AllocationAmount)
        {
            int? Tid = 0;
            //Retrieving Discipline
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Need Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;                
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();
                        
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SelClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Need Based"  && s.AridNo == Arid_No).FirstOrDefault();

                if(s != null)
                {    FaatApplication app = new FaatApplication();
                      app.status = "Reject";
                      app.ClassId = s.ClassId;
                      app.Id = s.ApplicationId;
                      _context.FaatApplication.Attach(app);
                      
                     _context.Entry(app).Property(x => x.status).IsModified = true;
                     _context.SaveChanges();
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                        Tid = s.Tid;
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    //Check Ledger Entry and Maintain Ledger
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {                        
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        
                        //I have changed here
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = 0;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        //StdClassDef.FaatScholarLog.ToList()[0].T = TRejectSchLedger;
                        StdClassDef.FaatScholarLog.ToList()[0].Tid = TRejectSchLedger.TransactionId;
                        Tid = TRejectSchLedger.TransactionId;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Tid).IsModified = true;
            
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Reject Found.
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        // I have change Allocation Amount to Null 
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = 0;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        Tid = null;
                        _context.SaveChanges();
                    }
                                        

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
            }
            
            //var link = Url.Action("NeedBasedList", "Home", new {Discipline=StdClassDef.Discipline } );
            //return Json(new { Url = link, status ="OK" });            
            double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);
            
            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= Tid
                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid = Tid
                });
            }
            
            
        }
        //MERIT BASED
        [HttpPost]
        public ActionResult AcceptMeritBased(String Arid_No, double AllocationAmount)
        {    int? Tid = 0;
            /*Retrieving Discipline*/
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                /* Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page*/
            }
            else
            {
                Semester = faatIntakeSeason.Year+faatIntakeSeason.IntakeSeason ;
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();
                       
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SelClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Merit Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,                            
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        Tid= s.Tid;
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                            Tid = s.T.TransactionId;
                        }
                    break;        
                }
            }

            if(AllocationAmount == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "",
                    DonationAccountStatus =  "",
                    status ="Error",
                    Message = "Please give valid amount!" 
                    
                });
            }
            

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    /*Check Ledger Entry and Maintain Ledger*/
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();           
                    }
                    else
                    {
                        /*Reject Found.*/
                    }
                                        
                    FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           

            
                    FaatScholarLog scholar = new FaatScholarLog()
                    {
                        Id = StdClassDef.FaatScholarLog.ToList()[0].Id,
                        ApplicationId = StdClassDef.FaatScholarLog.ToList()[0].ApplicationId,
                        AridNo = StdClassDef.FaatScholarLog.ToList()[0].AridNo,
                        Name = StdClassDef.FaatScholarLog.ToList()[0].Name,
                        ClassId = StdClassDef.FaatScholarLog.ToList()[0].ClassId,
                        Status = "Accept",
                        Type = StdClassDef.FaatScholarLog.ToList()[0].Type,
                        Cgpa = StdClassDef.FaatScholarLog.ToList()[0].Cgpa,
                        AllocationAmount  = AllocationAmount,
                        DefaultAmount = 0,
                        InsertionTimestamp = StdClassDef.FaatScholarLog.ToList()[0].InsertionTimestamp,
                        UpdateTimestamp = DateTime.Now
                    };
                    
                    scholar.T = null;         
                    scholar.Tid = TAcceptSchLedger.TransactionId;
                    
                    _context.Update(scholar);
                    _context.SaveChanges();
                    Tid = TAcceptSchLedger.TransactionId;

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
            }                       

            double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid = Tid
                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid= Tid
                });
            }            
        }

        [HttpPost]
        public ActionResult RejectMeritBased(String Arid_No, double? AllocationAmount)
        {
            int? Tid = 0;
            //Retrieving Discipline
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake and prepare scholarship list first) + Redirect to initial page
            }
            else
            {
               Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;
            }
            
            List<FaatClassDefinition> SelClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();
                        
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SelClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Merit Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    //Check Ledger Entry and Maintain Ledger
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {                        
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = 0;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        //StdClassDef.FaatScholarLog.ToList()[0].T = TRejectSchLedger;
                        StdClassDef.FaatScholarLog.ToList()[0].Tid = TRejectSchLedger.TransactionId;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Tid).IsModified = true;
                         Tid = TRejectSchLedger.TransactionId;
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Reject Found.
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = 0;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        
                        _context.SaveChanges();
                    }
                                        

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                }
            }
            
            //var link = Url.Action("NeedBasedList", "Home", new {Discipline=StdClassDef.Discipline } );
            //return Json(new { Url = link, status ="OK" });            
            double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);
            
            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= Tid
                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "",
                    Tid= Tid 
                });
            }
        }

        public bool AcceptAllMeritBasedAny(String Arid_No, double? AllocationAmount,
            List<FaatClassDefinition> SemesterClassDef)
        {               
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SemesterClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Merit Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    /*Check Ledger Entry and Maintain Ledger*/
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Accept Reversal",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();           
                    }
                    else
                    {
                        /*Reject Found.*/
                    }
                                        
                    FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           

            
                    FaatScholarLog scholar = new FaatScholarLog()
                    {
                        Id = StdClassDef.FaatScholarLog.ToList()[0].Id,
                        ApplicationId = StdClassDef.FaatScholarLog.ToList()[0].ApplicationId,
                        AridNo = StdClassDef.FaatScholarLog.ToList()[0].AridNo,
                        Name = StdClassDef.FaatScholarLog.ToList()[0].Name,
                        ClassId = StdClassDef.FaatScholarLog.ToList()[0].ClassId,
                        Status = "Accept",
                        Type = StdClassDef.FaatScholarLog.ToList()[0].Type,
                        Cgpa = StdClassDef.FaatScholarLog.ToList()[0].Cgpa,
                        AllocationAmount  = AllocationAmount,
                        DefaultAmount = 0,
                        InsertionTimestamp = StdClassDef.FaatScholarLog.ToList()[0].InsertionTimestamp,
                        UpdateTimestamp = DateTime.Now
                    };
                    
                    scholar.T = null;         
                    scholar.Tid = TAcceptSchLedger.TransactionId;
                    
                    _context.Update(scholar);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;                                               
        }

        [HttpPost]
        public ActionResult AcceptAllMeritBased(List<FaatClassDefinition> faatClassDefinition)
        {
            faatClassDefinition = faatClassDefinition.Where(e=>e.Semester !=null).ToList();
            Boolean isAmountValid = !faatClassDefinition.Any(c => c.FaatScholarLog.Any(s => s.AllocationAmount == null || s.AllocationAmount == 0 ));
            if(!isAmountValid)
            {
                TempData["Error"] = "Please give valid amount!";
            }
            else
            {
                
                /*Retrieving Semester*/
                FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
                FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
                
                String Semester = "";
                if(faatIntakeSeason == null)
                {                    
                    TempData["Error"] = "Define Intake and prepare scholarship list first.";
                }
                else
                {
                    Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;
                   /* if(faatIntakeSeason.IntakeSeason == "FM")
                    {
                        Semester = faatIntakeSeason.Year + "SM";
                        //SM with Same year
                    }   
                    else if(faatIntakeSeason.IntakeSeason == "SM")
                    {
                        //FM with Year - 1
                        Semester = (faatIntakeSeason.Year - 1) + "FM";
                    }
                    else
                    {
                        // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                    }
                   */
                    List<FaatClassDefinition> SemesterClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();

                    bool IsSuccess = true;
                    foreach(var cl in faatClassDefinition)
                    {
                        foreach(var std in cl.FaatScholarLog)
                        {
                            bool result = AcceptAllMeritBasedAny(std.AridNo, std.AllocationAmount, SemesterClassDef);
                            if(result == false)
                            {
                                IsSuccess = false;
                            }
                        }
                    }
                    if(IsSuccess == false)
                    {
                        TempData["Error"] = "Accept All was not successfully executed for all the students.";                    
                    }
                }
            }
            
            return RedirectToAction (nameof(MeritBasedList),new{Discipline=faatClassDefinition[0].Discipline});
        }


        public void RejectAllMeritBasedAny(String Arid_No, double? AllocationAmount, 
            List<FaatClassDefinition> SemisterClassDef)
        {                     
            FaatClassDefinition StdClassDef = null;
            foreach(var sel in SemisterClassDef)
            {
                FaatScholarLog s = sel.FaatScholarLog.Where( s => s.Type == "Merit Based"  && s.AridNo == Arid_No).FirstOrDefault();
                if(s != null)
                {
                    StdClassDef = new FaatClassDefinition(){
                       Id = sel.Id,
                       Semester = sel.Semester,
                       SemesterCount = sel.SemesterCount,
                       Discipline = sel.Discipline,
                       Section = sel.Section,
                       ClassStrength = sel.ClassStrength
                    };
                    StdClassDef.FaatScholarLog = new List<FaatScholarLog>();
                    StdClassDef.FaatScholarLog.Add(
                        new FaatScholarLog(){
                            Id = s.Id,
                            Tid = s.Tid,
                            ApplicationId = s.ApplicationId,
                            AridNo = s.AridNo,
                            Name = s.Name,
                            ClassId = s.ClassId,
                            Status = s.Status,
                            Type = s.Type,
                            Cgpa = s.Cgpa,
                            AllocationAmount = s.AllocationAmount,
                            DefaultAmount = s.DefaultAmount,
                            InsertionTimestamp = s.InsertionTimestamp,
                            UpdateTimestamp = s.UpdateTimestamp
                        }
                        );
                        if(s.T != null)
                        {
                            StdClassDef.FaatScholarLog.ToList()[0].T = new FaatScholarLedger()
                            {
                                TransactionId = s.T.TransactionId,
                                TransactionDate = s.T.TransactionDate,
                                Credit = s.T.Credit,
                                Debit = s.T.Debit,
                                Memo = s.T.Memo        
                            };
                        }
                    break;        
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {            
                try
                {
                    //Check Ledger Entry and Maintain Ledger
                    if(StdClassDef.FaatScholarLog.ToList()[0].T != null
                        && StdClassDef.FaatScholarLog.ToList()[0].Status == "Accept")
                    {                        
                        double? Credit = StdClassDef.FaatScholarLog.ToList()[0].T.Credit;
                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = AllocationAmount;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        //StdClassDef.FaatScholarLog.ToList()[0].T = TRejectSchLedger;
                        StdClassDef.FaatScholarLog.ToList()[0].Tid = TRejectSchLedger.TransactionId;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Tid).IsModified = true;
            
                        _context.SaveChanges();
                    }
                    else
                    {
                        //Reject Found.
                        _context.FaatScholarLog.Attach(StdClassDef.FaatScholarLog.ToList()[0]);

                        StdClassDef.FaatScholarLog.ToList()[0].Tid = null;
                        StdClassDef.FaatScholarLog.ToList()[0].T = null;
                        StdClassDef.FaatScholarLog.ToList()[0].AllocationAmount = AllocationAmount;
                        StdClassDef.FaatScholarLog.ToList()[0].Status = "Reject";
                        StdClassDef.FaatScholarLog.ToList()[0].UpdateTimestamp = DateTime.Now;

                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.AllocationAmount).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.UpdateTimestamp).IsModified = true;
                        _context.Entry(StdClassDef.FaatScholarLog.ToList()[0]).Property(x => x.Status).IsModified = true;
                        
                        _context.SaveChanges();
                    }                                        

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw e;
                    //return false;
                }
            }            
            //return true;
        }

        [HttpPost]
        public ActionResult RejectAllMeritBased(List<FaatClassDefinition> faatClassDefinition)
        {
            //Retrieving Semister
            FaatScholarshipStatus PendingNeedBasedAppStatus = _context.FaatScholarshipStatus.Where(s => s.Status == "Pending" && s.Type == "Merit Based").FirstOrDefault();
            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(s => s.Id == PendingNeedBasedAppStatus.IntakeSeasonId).FirstOrDefault();
            
            String Semester = "";
            if(faatIntakeSeason == null)
            {
                TempData["Error"] = "Define Intake and prepare scholarship list first.";
            }
            else
            {
                Semester = faatIntakeSeason.Year + faatIntakeSeason.IntakeSeason;
               /* if(faatIntakeSeason.IntakeSeason == "FM")
                {
                    Semester = faatIntakeSeason.Year + "SM";
                    //SM with Same year
                }   
                else if(faatIntakeSeason.IntakeSeason == "SM")
                {
                    //FM with Year - 1
                    Semester = (faatIntakeSeason.Year - 1) + "FM";
                }
                else
                {
                    // Set Error (Exception found while Defining Intake Season) + Redirect to initial page        
                }
                */

                List<FaatClassDefinition> SemisterClassDef = (from c in _context.FaatClassDefinition.AsNoTracking().Include(s => s.FaatScholarLog).ThenInclude(ss => ss.T) where c.Semester == Semester select c).ToList();

                bool IsSuccess = true;
                foreach(var cl in faatClassDefinition)
                {
                    foreach(var std in cl.FaatScholarLog)
                    {
                        try
                        {
                            std.AllocationAmount = std.AllocationAmount == null ? 0 : std.AllocationAmount; 
                            RejectAllMeritBasedAny(std.AridNo, std.AllocationAmount, SemisterClassDef);
                        }
                        catch(Exception e)
                        {
                            IsSuccess = false; 
                            continue;
                        }
                    }
                }

                if(IsSuccess == false)
                {
                    TempData["Error"] = "Reject All was not successfully executed for all the students.";           
                }
            }           
            
            return RedirectToAction (nameof(MeritBasedList),new{Discipline=faatClassDefinition[0].Discipline});
        }
        public IActionResult ViewApplication(int id)
        {
            FaatApplication application = _context.FaatApplication.Include(e=>e.FaatAppParentDetail).ThenInclude(e=>e.FaatAppGuardianOtherIncomeResourceFiles).Include(e=>e.FaatAppParentDetail).ThenInclude(e=>e.FaatAppMotherOtherIncomeResourceFiles).Include(e=>e.FaatAppResidenceInfo).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibStudent).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibJobHolder).Include(e=>e.FaatAppStudentDetail).Where(e=>e.Id==id).FirstOrDefault();
            return PartialView(application);
        } 
        [HttpGet]
        public IActionResult PreviousRecord(string id)
        {
           
            List<FaatIntakeSeason> faatIntakeSeasonList = _context.FaatIntakeSeason.Include(a => a.FaatScholarshipStatus).ToList();
            // we had given reference in this place           
            FaatIntakeSeason faatIntakeSeason = null;
            string policyJSON = "";            
            foreach(var i in faatIntakeSeasonList)
            {
                foreach( var j in i.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Merit Based")
                    {
                        policyJSON = j.Policy;
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
                return RedirectToAction("index");
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

             var email = User.Claims.Where(e => e.Type.Contains("emailaddress"))
                   .Select(e => e.Value).SingleOrDefault();
                var UserId = _context.Users.AsNoTracking().Where(e=>e.AridNo==email).FirstOrDefault().Id;   
              if(User.IsInRole("Admin") || User.IsInRole("Committee"))
              {
                   TempData["Param"] = _context.FaatScholarLog.Where(e=>e.AridNo==id).Include(e=>e.CD).Where(e=>e.CD.Semester == CurrentSemester).Select(e=>e.CD.Discipline).FirstOrDefault();
                    List<FaatScholarLog> PreviousData = _context.FaatScholarLog.Where(e=>e.AridNo==id).Include(e=>e.CD).Include(e=>e.A).Where(e=>e.CD.Semester!=CurrentSemester).ToList();
                    // Ask From Sir
                /*    List<FaatApplication> apps  = _context.FaatApplication.Include(e=>e.FaatClassDefinition).Where(e=>e.FaatClassDefinition.Semester!=CurrentSemester && e.AridNo ==id).ToList();
                    apps = apps.Where(e=>!PreviousData.Any(y=>y.ApplicationId==e.Id && y.Type == "Need Based")).ToList();
                   // List<FaatScholarLog> log = new List<FaatScholarLog>();

                    foreach(var item in apps)
                    {
                        FaatScholarLog obj = new FaatScholarLog();
                        obj.AridNo = item.AridNo;
                        obj.Cgpa = item.CGPA;
                        obj.AllocationAmount = 0;
                        obj.Status = item.status;
                        obj.CD = item.FaatClassDefinition;
                        obj.Type = "Need Based";
                        PreviousData.Add(obj);  

                    }
                   */
                    return View(PreviousData);
              }
              return View();
              
        }
       
        [HttpPost]
         public IActionResult AddComments(IEnumerable<FaatAppComments> comments)
        {
                  //  var email = User.Claims.Where(e => e.Type.Contains("emailaddress"))
                 //  .Select(e => e.Value).SingleOrDefault();
                 //   var user = _context.Users.Where(e=>e.AridNo.Contains(email.ToString())).FirstOrDefault();
               var Discipline = _context.FaatScholarLog.Where(e=>e.ApplicationId==comments.ToList()[0].ApplicationId).Include(e=>e.CD).Select(e=>e.CD.Discipline).ToList();
               if(comments.ToList()[0].Comments==null && comments.ToList()[0].Amount==0)
               {
                   if(_context.FaatAppComments.Where(e=>e.UserId==comments.ToList()[0].UserId && e.ApplicationId==comments.ToList()[0].ApplicationId && e.ClassID == comments.ToList()[0].ClassID).Count()>0)
                   {
                        _context.Remove(_context.FaatAppComments.Where(e=>e.UserId==comments.ToList()[0].UserId && e.ApplicationId==comments.ToList()[0].ApplicationId && e.ClassID==comments.ToList()[0].ClassID).FirstOrDefault());
                   }
                   
               }
               else
               {
                      
                if(_context.FaatAppComments.Where(e=>e.UserId==comments.ToList()[0].UserId && e.ApplicationId==comments.ToList()[0].ApplicationId && e.ClassID == comments.ToList()[0].ClassID).Count()>0)
                    {
                           var Record = _context.FaatAppComments.AsNoTracking().Where(e=>e.ApplicationId==comments.ToList()[0].ApplicationId && e.UserId==comments.ToList()[0].UserId && e.ClassID==comments.ToList()[0].ClassID).FirstOrDefault();
                         comments.ToList()[0].Id = Record.Id;
                         _context.Attach(comments.ToList()[0]);

                         _context.Entry(comments.ToList()[0]).Property("Comments").IsModified=true;
                           _context.Entry(comments.ToList()[0]).Property("Amount").IsModified=true;
                    }
                    else
                    {
                        _context.Add(comments.ToList()[0]);
                    }
               }
                       // _context.Attach(FaatAppComments.FirstOrDefault());
                        //_context.Entry(FaatAppComments.FirstOrDefault()).Property("Comments").IsModified = true;
                        _context.SaveChanges();
          //  FaatApplication application = _context.FaatApplication.Include(e=>e.FaatAppParentDetail).Include(e=>e.FaatAppResidenceInfo).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibStudent).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibJobHolder).Include(e=>e.FaatAppStudentDetail).Include(e=>e.FaatFiles).Include(e=>e.FaatAppComments).Where(e=>e.Id==id).FirstOrDefault();
           // return View(application);
       //    _context.AddRange(FaatAppComments);
        //   _context.SaveChanges();
    //       return RedirectToAction()
           return RedirectToAction("NeedBasedList",new{Discipline=Discipline.FirstOrDefault()});
        } 
        [HttpGet]
        public IActionResult History(string id)
        {
             string [] ID = id.Split("-");
             int ApplicationID = int.Parse(ID[0]);
             int ClassID = int.Parse(ID[1]); 
             var email = User.Claims.Where(e => e.Type.Contains("emailaddress"))
                   .Select(e => e.Value).SingleOrDefault();
             var UserId = _context.Users.AsNoTracking().Where(e=>e.AridNo==email).FirstOrDefault().Id;   
  
            if(User.IsInRole("Admin"))
            {
                 var Comments = _context.FaatAppComments.Include(e=>e.Users).Where(e=>e.ApplicationId==ApplicationID && e.ClassID==ClassID).ToList();
                  if(Comments.Count==0)
                 {
                     FaatAppComments comments = new FaatAppComments();
                     comments.ApplicationId = ApplicationID;
                     comments.UserId = UserId;
                     comments.ClassID = ClassID;
                     Comments.Add(comments); 
                 }
                 return PartialView(Comments);
            }
            else
            {
                
                var Comments = _context.FaatAppComments.Include(e=>e.Users).Where(e=>e.ApplicationId==ApplicationID && e.UserId==UserId && e.ClassID==ClassID).ToList();
                 if(Comments.Count==0)
                 {
                     FaatAppComments comments = new FaatAppComments();
                     comments.ApplicationId = ApplicationID;
                     comments.UserId = UserId;
                     comments.ClassID = ClassID;
                     Comments.Add(comments); 
                 }
                 return PartialView(Comments);
 
            }
            
        }
        public IActionResult AddComments(String id)
        {
              string [] ID = id.Split("-");
             int ApplicationID = int.Parse(ID[0]);
             int ClassID = int.Parse(ID[1]);  
            
            
            if(User.IsInRole("Admin"))
            {
                 var Comments = _context.FaatAppComments.Include(e=>e.Users).Include(e=>e.ClassDefinition).Where(e=>e.ApplicationId==ApplicationID && e.ClassID ==ClassID).ToList();
                 return View(Comments);
            }
            else
            {
                var email = User.Claims.Where(e => e.Type.Contains("emailaddress"))
                   .Select(e => e.Value).SingleOrDefault();
                var UserId = _context.Users.AsNoTracking().Where(e=>e.AridNo==email).FirstOrDefault().Id;   
                var Comments = _context.FaatAppComments.Include(e=>e.Users).Where(e=>e.ApplicationId==ApplicationID && e.UserId==UserId && e.ClassID==ClassID).ToList();
                 if(Comments.Count==0)
                 {
                     FaatAppComments comments = new FaatAppComments();
                     comments.ApplicationId = ApplicationID;
                     comments.UserId = UserId;
                    comments.ClassID = ClassID;
                     Comments.Add(comments); 
                 }
                 return PartialView(Comments);
 
            }
           
        }
        [HttpPost]
        public IActionResult ManualAddMerit(FaatClassDefinition faatClassDefinition)
        {
            if(faatClassDefinition.FaatScholarLog!=null)
            {  
            foreach(var item in faatClassDefinition.FaatScholarLog)
            {
                if(item.IsManual==true)
                {
                item.Status="Pending";
                item.Type="Merit Based";
                item.InsertionTimestamp=DateTime.Now;
                item.UpdateTimestamp = DateTime.Now;
                }
               
            }
            
            _context.AddRange(faatClassDefinition.FaatScholarLog.Where(e=>e.IsManual==true));
            _context.SaveChanges();
            }
            return RedirectToAction (nameof(MeritBasedList),new{Discipline=faatClassDefinition.Discipline});
        }

        public IActionResult ManualAddMerit(string id)
        {
            DBAccessLayer obj = new DBAccessLayer(configuration,_context);
            
            //Determine the Semeter for fetching Results
            List<FaatIntakeSeason> faatIntakeSeasonList = _context.FaatIntakeSeason.Include(a => a.FaatScholarshipStatus).ToList();
            // we had given reference in this place           
            FaatIntakeSeason faatIntakeSeason = null;
            string policyJSON = "";            
            foreach(var i in faatIntakeSeasonList)
            {
                foreach( var j in i.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Merit Based")
                    {
                       // policyJSON = j.Policy;
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
                return RedirectToAction("index");
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

            string [] ClassDesc = id.Split("-");

            List<FaatScholarLog> RemainingStds = null;            
             
            double? cgpa = _context.FaatPolicy.AsNoTracking().Where(e=>e.IsSelected==1).Select(e=>e.MeritMinCGPA).FirstOrDefault();
            
            RemainingStds = obj.RemainingMeritStudentList(PreviousSemester,CurrentSemester, cgpa, ClassDesc);
            
            var ClassDef = _context.FaatClassDefinition.Where(e=>e.Id== int.Parse(ClassDesc[3])).FirstOrDefault();
            var ClassID = ClassDef.Id;
            RemainingStds.ForEach(e=>e.ClassId=ClassID);
            ClassDef.FaatScholarLog = RemainingStds;
            ClassDef.FaatScholarLog = ClassDef.FaatScholarLog.OrderByDescending(e=>e.Cgpa).ToList();
            return PartialView(ClassDef);
        }
        [HttpPost]
        public IActionResult ManualAddNeed(FaatClassDefinition faatClassDefinition)
        {
            foreach(var item in faatClassDefinition.FaatScholarLog)
            {
               if(item.IsManual==true)
               {
                item.Status = "Pending";
                item.Type = "Need Based";
                item.InsertionTimestamp= DateTime.Now;
                item.UpdateTimestamp = DateTime.Now;
               }
            }
           // _context.AddRange(faatClassDefinition.FaatScholarLog.Where(e=>e.IsManual==true).ToList());
             _context.AddRange(faatClassDefinition.FaatScholarLog.Where(e=>e.IsManual==true));
            _context.SaveChanges();
            return RedirectToAction (nameof(NeedBasedList),new{Discipline=faatClassDefinition.Discipline});

        }
        public IActionResult ManualAddNeed(string id)
        {
              //Determine the Semeter for fetching Results
            List<FaatIntakeSeason> faatIntakeSeasonList = _context.FaatIntakeSeason.Include(a => a.FaatScholarshipStatus).ToList();
            // we had given reference in this place           
            FaatIntakeSeason faatIntakeSeason = null;
            string policyJSON = "";            
            foreach(var i in faatIntakeSeasonList)
            {
                foreach( var j in i.FaatScholarshipStatus)
                {
                    if(j.Status == "Pending" && j.Type == "Need Based")
                    {
                       // policyJSON = j.Policy;
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
                return RedirectToAction("index");
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

            string [] ClassDesc = id.Split("-");

                      
            var Scholars = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==ClassDesc[0] && e.SemesterCount==ClassDesc[1] && e.Section==ClassDesc[2] && e.Semester==CurrentSemester).Include(e=>e.FaatScholarLog).FirstOrDefault();
            double? cgpa = _context.FaatPolicy.AsNoTracking().Where(e=>e.IsSelected==1).Select(e=>e.NeedMinCGPA).FirstOrDefault();
            var TotalClassApplications = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==ClassDesc[0] && e.SemesterCount==ClassDesc[1] && e.Section==ClassDesc[2] && e.Semester==CurrentSemester).Include(e=>e.FaatApplications).FirstOrDefault();

            TotalClassApplications.FaatApplications = TotalClassApplications.FaatApplications.Where(e=>e.status=="Pending" &&  !Scholars.FaatScholarLog.Select(x=>x.ApplicationId).Contains(e.Id)).ToList();
            
            var TotalShortListedApplications = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==ClassDesc[0] && e.SemesterCount==ClassDesc[1] && e.Section==ClassDesc[2] && e.Semester==CurrentSemester).Include(e=>e.FaatScholarLog).FirstOrDefault();
           //  var ID = TotalShortListedApplications.FaatScholarLog.Where(e=>e.Type=="Need Based").Select(e=>e.ApplicationId);
            // var otherObjects = TotalClassApplications.FaatApplications.Where(x => !ID.Contains(x.Id));
                
                List<FaatScholarLog> list = new List<FaatScholarLog>();

                foreach (var item in TotalClassApplications.FaatApplications)
                {
                    
                       
                    FaatScholarLog obj = new FaatScholarLog();
                    obj.ClassId = item.ClassId;
                    obj.Name = item.Name;
                    obj.AridNo = item.AridNo;
                    obj.Cgpa = item.CGPA;
                    obj.ApplicationId = item.Id;
                    list.Add(obj);
                        
                    
                }
              
               // TotalShortListedApplications.FaatScholarLog.Clear();
                TotalShortListedApplications.FaatScholarLog = list;
               // TotalShortListedApplications.FaatScholarLog = TotalShortListedApplications.FaatScholarLog.OrderByDescending(e=>e.Cgpa).ToList();
            return PartialView(TotalShortListedApplications);
        }
        [HttpPost]
        public IActionResult AcceptMeritClass(String Class)
        {
            FaatClassDefinition AcceptedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<FaatClassDefinition>(Class);
            foreach(var item in AcceptedClass.FaatScholarLog)
            {
                if(item.AllocationAmount == 0 || item.AllocationAmount == null)
                {
                    return Json(new 
                { 
                    DonationAccountVal = "",
                    DonationAccountStatus =  "",
                    status ="Error",
                    Message = "Please give valid amount!" 
                });
                

                }
            }

             foreach(var item in AcceptedClass.FaatScholarLog)
             {
                    if(item.Status == "Reject")
                    {
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = 0,
                            Credit = item.AllocationAmount,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = item.AllocationAmount,
                            Credit = 0,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();
                        item.Tid = TReversalSchLedger.TransactionId;
                        item.T = TReversalSchLedger;
                        item.Status = "Accept";

                    }
                    if(item.Status=="Pending")
                    {
                        FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = item.AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = item.AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           
                     item.Tid = TAcceptSchLedger.TransactionId;
                     item.T = TAcceptSchLedger;
                     item.Status = "Accept";
                    }
                    

             }
             _context.Update(AcceptedClass);
             _context.SaveChanges();

              double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= AcceptedClass.FaatScholarLog.Select(e=>e.Tid)
                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid= AcceptedClass.FaatScholarLog.Select(e=>e.Tid)

                });
            }



            

                 
           
        }

        [HttpPost]
        public IActionResult RejectMeritClass(String Class)
        {
            FaatClassDefinition RejectedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<FaatClassDefinition>(Class);
             foreach(var item in RejectedClass.FaatScholarLog)
             {
                 item.AllocationAmount = 0;
                 
             }
             
             foreach(var item in RejectedClass.FaatScholarLog)
             {
                    if(item.Status == "Accept")
                    {
                        double? Credit = _context.FaatScholarLedger.Where(e=>e.TransactionId == item.Tid).FirstOrDefault().Credit;

                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        item.Tid = TRejectSchLedger.TransactionId;
                        item.T = TRejectSchLedger;
                        item.Status = "Reject";

                    }
                 /*  if(item.Status == "Reject")
                    {
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = 0,
                            Credit = item.AllocationAmount,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = item.AllocationAmount,
                            Credit = 0,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();
                        item.Tid = TReversalSchLedger.TransactionId;
                        item.Status = "Reject";

                    }
                    */
                    if(item.Status=="Pending")
                    {
                      item.Status = "Reject";

                    }
             }
             _context.Update(RejectedClass);
             _context.SaveChanges();

              double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= RejectedClass.FaatScholarLog.Select(e=>e.Tid)

                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid= RejectedClass.FaatScholarLog.Select(e=>e.Tid)

                });
            }



            

                 
           
        }

      [HttpPost]
        public IActionResult AcceptNeedClass(String Class)
        {
           
            FaatClassDefinition AcceptedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<FaatClassDefinition>(Class);
          //  var Profiles = _context.FaatApplication.Where(e => AcceptedClass.FaatScholarLog.Select(e=>e.ApplicationId).Contains(e.Id));
            foreach(var item in AcceptedClass.FaatScholarLog)
            {
                if(item.AllocationAmount == 0 || item.AllocationAmount == null)
                {
                    return Json(new 
                { 
                    DonationAccountVal = "",
                    DonationAccountStatus =  "",
                    status ="Error",
                    Message = "Please give valid amount!" 
                });
                

                }
            }

             foreach(var item in AcceptedClass.FaatScholarLog)
             {
                    if(item.Status == "Reject")
                    {
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = 0,
                            Credit = item.AllocationAmount,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = item.AllocationAmount,
                            Credit = 0,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();
                        item.Tid = TReversalSchLedger.TransactionId;
                        item.Status = "Accept";
                        item.A.status = "Accept";
                        

                    }
                    if(item.Status=="Pending")
                    {
                        FaatScholarLedger TAcceptSchLedger= new FaatScholarLedger()
                    {
                        Debit = 0,
                        Credit = item.AllocationAmount,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now
                    };        
                    _context.Add(TAcceptSchLedger);
                    _context.SaveChanges();           

                    DonationLedger TAcceptDonLedger = new DonationLedger()
                    {
                        Debit = item.AllocationAmount,
                        Credit = 0,
                        Memo = "Accept",
                        TransactionDate = DateTime.Now        
                    };
                    _context.Add(TAcceptDonLedger);
                    _context.SaveChanges();           
                     item.Tid = TAcceptSchLedger.TransactionId;
                     item.Status = "Accept";
                     item.A.status = "Accept";
                    }
                    

             }
            
              List<FaatScholarLog> list = AcceptedClass.FaatScholarLog.ToList();
            //  List<FaatApplication> list1 = AcceptedClass.FaatApplications.ToList();
               _context.UpdateRange(AcceptedClass.FaatScholarLog);
             _context.SaveChanges();
             //_context.UpdateRange(list);
            // _context.SaveChanges();
            
          //   _context.Update(AcceptedClass.FaatApplications.ToList());
          //   _context.SaveChanges();


              double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid = JsonConvert.SerializeObject(AcceptedClass.FaatScholarLog.Select(e=>e.Tid).ToList())
                });
            }       
            else
            {                          var Tid = JsonConvert.SerializeObject(AcceptedClass.FaatScholarLog.Select(e=>e.Tid));
         
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid= AcceptedClass.FaatScholarLog.Select(e=>e.Tid.ToString())
                
                });
            }



            

                 
           
        }

        [HttpPost]
        public IActionResult RejectNeedClass(String Class)
        {
            FaatClassDefinition RejectedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<FaatClassDefinition>(Class);
             foreach(var item in RejectedClass.FaatScholarLog)
             {
                 item.AllocationAmount = 0;
                 
             }
             
             foreach(var item in RejectedClass.FaatScholarLog)
             {
                    if(item.Status == "Accept")
                    {
                         double? Credit = _context.FaatScholarLedger.Where(e=>e.TransactionId == item.Tid).FirstOrDefault().Credit;
                        FaatScholarLedger TRejectSchLedger= new FaatScholarLedger()
                        {
                            Debit = Credit,
                            Credit = 0,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now
                        };        
                        _context.Add(TRejectSchLedger);
                        _context.SaveChanges();           

                        DonationLedger TRejectDonLedger = new DonationLedger()
                        {
                            Debit = 0,
                            Credit = Credit,
                            Memo = "Reject",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TRejectDonLedger);
                        _context.SaveChanges();           
                        
                        item.Tid = TRejectSchLedger.TransactionId;
                        item.T = TRejectSchLedger;
                        item.Status = "Reject";
                        item.A.status = "Reject";

                    }
                /*    if(item.Status == "Reject")
                    {
                        FaatScholarLedger TReversalSchLedger = new FaatScholarLedger()
                        {
                            Debit = 0,
                            Credit = item.AllocationAmount,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now    
                        };
                        _context.Add(TReversalSchLedger);
                        _context.SaveChanges();                                       

                        DonationLedger TReversalDonLedger = new DonationLedger()
                        {
                            Debit = item.AllocationAmount,
                            Credit = 0,
                            Memo = "Accept",
                            TransactionDate = DateTime.Now        
                        };
                        _context.Add(TReversalDonLedger);
                        _context.SaveChanges();
                        item.Tid = TReversalSchLedger.TransactionId;
                        item.Status = "Reject";
                        item.A.status = "Reject";

                    }*/
                    if(item.Status=="Pending")
                    {
                      item.Status = "Reject";
                      item.A.status = "Reject";

                    }
             }
             _context.UpdateRange(RejectedClass.FaatScholarLog.ToList());
             _context.SaveChanges();

              double result = 0; 
            double.TryParse(_context.DonationLedger.Sum(c => c.Credit - c.Debit).ToString(), out result);

            if(result == 0)
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. Zero",
                    DonationAccountStatus =  "Balance (" + "Zero" + ")",
                    status ="OK",
                    Message = "" ,
                    Tid= RejectedClass.FaatScholarLog.Select(e=>e.Tid)

                });
            }       
            else
            {                
                return Json(new 
                { 
                    DonationAccountVal = "Rs. " + (result < 0 ? result * -1 : result),
                    DonationAccountStatus = "Balance (" + (result < 0 ? "Debit" :"Credit") +")",
                    status ="OK",
                    Message = "" ,
                    Tid= RejectedClass.FaatScholarLog.Select(e=>e.Tid)

                });
            }



            

                 
           
        }

      
        
       

    }
}

