using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models;
using FinancialAidAllocationTool.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using FinancialAidAllocationTool.Models.IntakeSeason;
using Z.EntityFramework.Plus;
using System.IO;
using Microsoft.AspNetCore.Http;
using FinancialAidAllocationTool.Models.Policy;




namespace FinancialAidAllocationTool.Controllers
{

    public class ApplicationController : Controller
    {
        private readonly FaaToolDBContext _context;
        private readonly IConfiguration configuration;
        public DBAccessLayer DbManager;

        public ApplicationController(FaaToolDBContext context,IConfiguration configuration)
        {
            this.configuration = configuration;
            
            _context = context;
            DbManager = new DBAccessLayer(this.configuration,_context);
        }

        // GET: Application
        public IActionResult Parent()
        {
            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(i => i.FaatScholarshipStatus.Any( s=>s.Status=="Pending" && s.Type=="Need Based")).FirstOrDefault();
            //FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.IncludeFilter(a=>a.FaatScholarshipStatus.Where(e=>e.Status=="Pending" && e.Type=="Need Based")).FirstOrDefault();

            String PreviousSemester = "";
            String CurrentSemester = "";
               if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake first) + Redirect to initial page
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
                }
            }   
              FaatClassDefinition classDefinition = DbManager.CurrentNeedBasedStudent(faatIntakeSeason.Year+faatIntakeSeason.IntakeSeason,AridNo);
              FaatClassDefinition dbClass = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==classDefinition.Discipline && e.Section==classDefinition.Section && e.Semester==classDefinition.Semester && e.SemesterCount==classDefinition.SemesterCount).FirstOrDefault();
              TempData["FatherName"] = DbManager.FatherName(AridNo);
              if(dbClass!=null)
              {
                  FaatApplication FaatApplication = _context.FaatApplication.Where(e=>e.AridNo==AridNo && e.ClassId==dbClass.Id).Include(e=>e.FaatClassDefinition).Include(e=>e.FaatScholarLog).FirstOrDefault();
                  FaatApplication.FaatScholarLog = FaatApplication.FaatScholarLog.Where(e=>e.ClassId==dbClass.Id && e.Type == "Need Based").ToList();
                  return View (FaatApplication);
              
              }
            //var UserID = _context.Users.ToList().Where(d=>d.AridNo==AridNo).Select(d=>d.Id).SingleOrDefault();
            

            return View();
        }
        public IActionResult UserProfile(String Param)
        {
            TempData["Param"] = Param;
            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(i => i.FaatScholarshipStatus.Any( s=>s.Status=="Pending" && s.Type=="Need Based")).FirstOrDefault();
            //FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.IncludeFilter(a=>a.FaatScholarshipStatus.Where(e=>e.Status=="Pending" && e.Type=="Need Based")).FirstOrDefault();

            String PreviousSemester = "";
            String CurrentSemester = "";
               if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake first) + Redirect to initial page
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
                }
            }                
           
            //var UserID = _context.Users.ToList().Where(d=>d.AridNo==AridNo).Select(d=>d.Id).SingleOrDefault();
            var faaToolDBContext = _context.FaatApplication.Where(d=>d.AridNo == AridNo).Include(e=>e.FaatAppStudentDetail).Include(e=>e.FaatAppParentDetail).ThenInclude(e=>e.FaatAppGuardianOtherIncomeResourceFiles).Include(e=>e.FaatAppParentDetail).ThenInclude(e=>e.FaatAppMotherOtherIncomeResourceFiles).Include(e=>e.FaatAppResidenceInfo).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibJobHolder).Include(e=>e.FaatAppSiblingInfo).ThenInclude(e=>e.FaatAppSibStudent).Include(e=>e.FaatScholarLog).ThenInclude(e=>e.CD).OrderByDescending(e=>e.InsertionTimestamp).FirstOrDefault();
            TempData["Name"]=DbManager.FindName(AridNo);

            FaatClassDefinition classDefinition = DbManager.CurrentNeedBasedStudent(faatIntakeSeason.Year+faatIntakeSeason.IntakeSeason,AridNo);
                FaatClassDefinition dbClass = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==classDefinition.Discipline && e.Section==classDefinition.Section && e.Semester==classDefinition.Semester && e.SemesterCount==classDefinition.SemesterCount).FirstOrDefault();
                if(dbClass!=null)
                { 

                 if(dbClass.Id != faaToolDBContext.ClassId) 
                 {
                     faaToolDBContext.status = "";
                     faaToolDBContext.ClassId = dbClass.Id;
                     _context.Update(faaToolDBContext);
                     _context.SaveChanges();

                 }  
                 

                }             
                
            FaatAppStudentDetail _objModel= DbManager.StudentDetail(CurrentSemester,PreviousSemester,AridNo);
            
            faaToolDBContext.FaatAppStudentDetail.ToList()[0].Semester = _objModel.Semester;
            faaToolDBContext.FaatAppStudentDetail.ToList()[0].Section = _objModel.Section;
            faaToolDBContext.FaatAppStudentDetail.ToList()[0].Cpga = _objModel.Cpga;
            faaToolDBContext.FaatAppStudentDetail.ToList()[0].Class = _objModel.Class;



            //history
            faaToolDBContext.FaatScholarLog = faaToolDBContext.FaatScholarLog.Where(e=>e.CD.Semester != CurrentSemester).ToList();
            //
            faaToolDBContext.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles = faaToolDBContext.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles.Where(e=>e.Income!=null || e.ResourceType!=null || e.FileData !=null || e.FileName != null || e.FileType != null).ToList();
            faaToolDBContext.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles = faaToolDBContext.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles.Where(e=>e.Income!=null || e.ResourceType!=null || e.FileData !=null || e.FileName != null || e.FileType != null).ToList();
            faaToolDBContext.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder =faaToolDBContext.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder.Where(e=>e.Name != null || e.MonthlyIncome != null || e.Designation != null || e.ContractFileType != null || e.ContractFileName != null || e.ContractFileData != null || e.Company != null).ToList();
            faaToolDBContext.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent = faaToolDBContext.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent.Where(e=>e.ClassInstituteName != null || e.Name != null || e.StdentCardFileData != null || e.StdentCardFileName != null || e.StdentCardFileType !=null).ToList(); 
           
           
            return View(faaToolDBContext);
        }
        public IActionResult Index(String Param)
        {   
            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            if(_context.FaatApplication.Where(e=>e.AridNo == AridNo).Count()>0)
            {
                
                return RedirectToAction(nameof(UserProfile),new{Param = Param});
                
            }  
            else
            {
            return View();
            }
        }
       

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            // var faatApplication = await _context.FaatApplication
            //     .Include(f => f.User)
            //     .FirstOrDefaultAsync(m => m.Id == id);
            // if (faatApplication == null)
            // {
            //     return NotFound();
            // }

            // return View(faatApplication);
            return NotFound();
        }
        [HttpPost]
        public IActionResult FileSection(IFormFile files)
        {
            FaatFiles File = new FaatFiles();
             System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
            File.FileData = encoding.GetBytes(files.Length.ToString());
            File.FileName  = files.FileName;
            File.FileType = files.ContentType;

                            if (files.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    files.CopyTo(stream);
                                    
                                    
                                    
                                    File.FileData = stream.ToArray();
                                    
                                 }
                             }
            
            return PartialView(File);
        }
        // GET: Application/Create
        public IActionResult Create()
        {
            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            //Query and Get Student Info

            //Check 1 = Get Open Pending Scholarship Intake for Need Based
            //FaatIntakeSeason Intake = _context.FaatIntakeSeason.IncludeFilter(a=>a.FaatScholarshipStatus.Where(e=>e.Status=="Pending" && e.Type=="Need Based")).ToList().Where(i => i.FaatScholarshipStatus.Any(s => s.Status=="Pending" && s.Type=="Need Based")).FirstOrDefault();

            FaatIntakeSeason Intake = _context.FaatIntakeSeason.Where(i => i.FaatScholarshipStatus.Any( s=>s.Status=="Pending" && s.Type=="Need Based")).FirstOrDefault();
            
            //FaatIntakeSeason Intake = IntakeList[0];

            //Check 2 = Check if student is enrolled for the semister
            bool IsStudentEnrolled = DbManager.IsStudentEnrolled(Intake.Year+Intake.IntakeSeason,AridNo);

            //Check 3 = Check if Class exists in FaatToolDB Class Definition
            FaatClassDefinition classDefinition = DbManager.CurrentNeedBasedStudent(Intake.Year+Intake.IntakeSeason,AridNo);
            FaatClassDefinition faatDBClass = _context.FaatClassDefinition.Where(e=>e.Discipline==classDefinition.Discipline && e.Section==classDefinition.Section && e.Semester==classDefinition.Semester && e.SemesterCount==classDefinition.SemesterCount).FirstOrDefault();

            //Check 4 = Check if Application already exists of the student for the semister
            FaatApplication ExistingApplication = _context.FaatApplication.Where( a => a.FaatAppStudentDetail.Any( s => s.AridNo == AridNo && s.Semester == Intake.Year+Intake.IntakeSeason)).FirstOrDefault();

            
            if(Intake != null 
            && IsStudentEnrolled 
            && faatDBClass != null 
            && ExistingApplication == null)
            {
                return View(new FaatApplication());
            }
            else
            {
                if(Intake == null)
                {
                    TempData["Intake"]="<script>alert('Scholarship is not open.');</script>";
                }
                else if(!IsStudentEnrolled)
                {
                    TempData["CurrentSemesterStudent"] = "<script>alert('Student is not enrolled in the current semester');</script>";
                }
                else if(faatDBClass == null)
                {
                    TempData["faatDBClass"] = "<script>alert('Class Definition does not exist. Please ask admin to perform Shortlisting Process.');</script>";    
                }
                else if(ExistingApplication != null)
                {
                    TempData["ExistingApplication"] = "<script>alert('Application already exist of the student for the semister.');</script>";        
                }
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public IActionResult Save([FromForm] FaatApplication faatApplication,String Apply,String Param)
        {
           
            if(faatApplication!=null)
            { 
                  if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles!=null)
                  {
                  faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles = faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles.Where(e=>e!=null).ToList();
                  }
                  if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles!=null)
                  {
                  faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles = faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles.Where(e=>e!=null).ToList();
                  }
                  if(faatApplication.FaatAppSiblingInfo.ToList()[0]!=null)
                  {
                    foreach(var item in faatApplication.FaatAppSiblingInfo.ToList())
                 {
                    if(item.FaatAppSibJobHolder!=null)
                    {
                    item.FaatAppSibJobHolder = item.FaatAppSibJobHolder.Where(e=>e!=null).ToList();
                    }
                    if(item.FaatAppSibStudent!=null)
                    {
                    item.FaatAppSibStudent = item.FaatAppSibStudent.Where(e=>e!=null).ToList();
                    }
                 }
                  }
                System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
                if( faatApplication.FaatAppParentDetail.ToList()[0].FatherFile!=null)
                {
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileData = encoding.GetBytes( faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.Length.ToString());
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileName = faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.FileName;
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileType =faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.ContentType;
                             if (faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileData = stream.ToArray();
                                    
                                 }
                             }
                }
                if(faatApplication.file!=null)
                {
                    faatApplication.UserImage = encoding.GetBytes( faatApplication.file.Length.ToString());
                             faatApplication.UserImageFileName = faatApplication.file.FileName;
                             faatApplication.UserImageFileType =faatApplication.file.ContentType;
                            if (faatApplication.file.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.file.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.UserImage = stream.ToArray();
                                    
                                 }
                             }

                }  



                if(faatApplication.FaatAppParentDetail.ToList()[0].MotherFile!=null)
                {
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileData = encoding.GetBytes( faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.Length.ToString());
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileName = faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.FileName;
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileType =faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.ContentType;
                            if (faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileData = stream.ToArray();
                                    
                                 }
                             }
                }
                             if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles!=null)
                             {
                             foreach(var file in faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles)
                              {
                                  if(file.file!=null)
                                  {
                                  file.FileData =encoding.GetBytes( file.file.Length.ToString()); 
                                  file.FileName = file.file.FileName;
                                  file.FileType = file.file.ContentType;
                                   if (file.file.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.file.CopyTo(stream);
                                    
                                    
                                    
                                             file.FileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              } 
                             }   
                             if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles!=null)
                             {
                               foreach(var file in faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles)
                              {
                                  if(file.file!=null)
                                  {
                                  file.FileData =encoding.GetBytes( file.file.Length.ToString()); 
                                  file.FileName = file.file.FileName;
                                  file.FileType = file.file.ContentType;
                                   if (file.file.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.file.CopyTo(stream);
                                    
                                    
                                    
                                             file.FileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              }  
                             } 
                              if(faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile!=null)
                              {
                              faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileData = encoding.GetBytes( faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.Length.ToString());
                             faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileName = faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.FileName;
                             faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileType =faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.ContentType;
                              if (faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileData = stream.ToArray();
                                    
                                 }
                             }
                              }
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0]!=null)
                              {
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder!=null)
                              {
                              foreach(var file in faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder)
                              {   if(file.ContractFile!=null)
                              {
                                  file.ContractFileData =encoding.GetBytes( file.ContractFile.Length.ToString()); 
                                  file.ContractFileName = file.ContractFile.FileName;
                                  file.ContractFileType = file.ContractFile.ContentType;
                                   if (file.ContractFile.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.ContractFile.CopyTo(stream);
                                    
                                    
                                    
                                             file.ContractFileData = stream.ToArray();
                                    
                                        }
                                    }
                              }
                              }  
                              }
                              
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent!=null)
                              { 

                               foreach(var file in faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent)
                              {
                                  if(file.StudentCardFile!=null)
                                  {
                                  file.StdentCardFileData =encoding.GetBytes( file.StudentCardFile.Length.ToString()); 
                                  file.StdentCardFileName = file.StudentCardFile.FileName;
                                  file.StdentCardFileType = file.StudentCardFile.ContentType;
                                   if (file.StudentCardFile.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.StudentCardFile.CopyTo(stream);
                                    
                                    
                                    
                                             file.StdentCardFileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              }   
                                      
            }
            }

               if(Apply == null)
               {
                _context.Update(faatApplication);
                _context.SaveChanges();
               }
               else if(faatApplication.status == "" || faatApplication.status == null)
               {
                   FaatIntakeSeason Intake = _context.FaatIntakeSeason.AsNoTracking().IncludeFilter(e=>e.FaatScholarshipStatus.Where(e=>e.Type=="Need Based")).OrderByDescending(e=>e.InsertionTimestamp).FirstOrDefault();

            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            
            faatApplication.InsertionTimestamp = DateTime.Now;
            //faatApplication.Status = "Pending";
            faatApplication.UpdateTimestamp = DateTime.Now;                
            
             

                FaatClassDefinition classDefinition = DbManager.CurrentNeedBasedStudent(Intake.Year+Intake.IntakeSeason,AridNo);
                FaatClassDefinition dbClass = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==classDefinition.Discipline && e.Section==classDefinition.Section && e.Semester==classDefinition.Semester && e.SemesterCount==classDefinition.SemesterCount).FirstOrDefault();
                if(dbClass!=null)
                {
                 faatApplication.CGPA = faatApplication.FaatAppStudentDetail.FirstOrDefault().Cpga;
                 faatApplication.AridNo = faatApplication.FaatAppStudentDetail.FirstOrDefault().AridNo;
                 faatApplication.Name = faatApplication.FaatAppStudentDetail.FirstOrDefault().Name;
                 faatApplication.ClassId =  dbClass.Id; 
                 faatApplication.status = "Pending"; 

                _context.Update(faatApplication);
                _context.SaveChanges();
                }
                else
                {
                    faatApplication.CGPA = faatApplication.FaatAppStudentDetail.FirstOrDefault().Cpga;
                 faatApplication.AridNo = faatApplication.FaatAppStudentDetail.FirstOrDefault().AridNo;
                 faatApplication.Name = faatApplication.FaatAppStudentDetail.FirstOrDefault().Name;
                 faatApplication.ClassId =  dbClass.Id; 
                 faatApplication.status = "Pending"; 

                _context.Update(faatApplication);
                _context.SaveChanges();


                }
                FaatPolicy policy = _context.FaatPolicy.Include(e=>e.FaatRule).ThenInclude(e=>e.FaatRuleDescription).Where(e=>e.IsSelected==1).FirstOrDefault();
                if(faatApplication.CGPA>=policy.NeedMinCGPA)
                {
                
                FaatScholarLog NeedBasedScholar = new FaatScholarLog();
                NeedBasedScholar.InsertionTimestamp = DateTime.Now;
                NeedBasedScholar.UpdateTimestamp = DateTime.Now;

                NeedBasedScholar.Name = faatApplication.Name;
                NeedBasedScholar.Status = "Pending";                
                NeedBasedScholar.Type = "Need Based";
                NeedBasedScholar.AridNo=AridNo;
                NeedBasedScholar.ClassId=dbClass.Id;

                                
                NeedBasedScholar.ApplicationId = faatApplication.Id;
                NeedBasedScholar.Cgpa = faatApplication.CGPA;
                _context.Add(NeedBasedScholar);
                _context.SaveChanges();
                }


               
            }
            }
            return RedirectToAction(nameof(Index),new{Param = Param});
        }
    

        // POST: Application/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] FaatApplication faatApplication)
        { 
              
            
         

                          

            if(faatApplication!=null)
            { 
                  if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles!=null)
                  {
                  faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles = faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles.Where(e=>e!=null).ToList();
                  }
                  if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles!=null)
                  {
                  faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles = faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles.Where(e=>e!=null).ToList();
                  }
                  if(faatApplication.FaatAppSiblingInfo.ToList()[0]!=null)
                  {
                    foreach(var item in faatApplication.FaatAppSiblingInfo.ToList())
                 {
                    if(item.FaatAppSibJobHolder!=null)
                    {
                    item.FaatAppSibJobHolder = item.FaatAppSibJobHolder.Where(e=>e!=null).ToList();
                    }
                    if(item.FaatAppSibStudent!=null)
                    {
                    item.FaatAppSibStudent = item.FaatAppSibStudent.Where(e=>e!=null).ToList();
                    }
                 }
                  }
                System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
                if(faatApplication.file!=null)
                {
                    faatApplication.UserImage = encoding.GetBytes( faatApplication.file.Length.ToString());
                faatApplication.UserImageFileName = faatApplication.file.FileName;
                faatApplication.UserImageFileType =faatApplication.file.ContentType;
                             if (faatApplication.file.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.file.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.UserImage = stream.ToArray();
                                    
                                 }
                             }
                    

                }  



                if( faatApplication.FaatAppParentDetail.ToList()[0].FatherFile!=null)
                {
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileData = encoding.GetBytes( faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.Length.ToString());
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileName = faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.FileName;
                faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileType =faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.ContentType;
                             if (faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppParentDetail.ToList()[0].FatherFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppParentDetail.ToList()[0].FatherCNICDeathCertificateFileData = stream.ToArray();
                                    
                                 }
                             }
                }
                if(faatApplication.FaatAppParentDetail.ToList()[0].MotherFile!=null)
                {
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileData = encoding.GetBytes( faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.Length.ToString());
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileName = faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.FileName;
                             faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileType =faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.ContentType;
                            if (faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppParentDetail.ToList()[0].MotherFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppParentDetail.ToList()[0].MotherCNICDeathCertificateFileData = stream.ToArray();
                                    
                                 }
                             }
                }
                             if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles!=null)
                             {
                             foreach(var file in faatApplication.FaatAppParentDetail.ToList()[0].FaatAppGuardianOtherIncomeResourceFiles)
                              {
                                  if(file.file!=null)
                                  {
                                  file.FileData =encoding.GetBytes( file.file.Length.ToString()); 
                                  file.FileName = file.file.FileName;
                                  file.FileType = file.file.ContentType;
                                   if (file.file.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.file.CopyTo(stream);
                                    
                                    
                                    
                                             file.FileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              } 
                             }   
                             if(faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles!=null)
                             {
                               foreach(var file in faatApplication.FaatAppParentDetail.ToList()[0].FaatAppMotherOtherIncomeResourceFiles)
                              {
                                  if(file.file!=null)
                                  {
                                  file.FileData =encoding.GetBytes( file.file.Length.ToString()); 
                                  file.FileName = file.file.FileName;
                                  file.FileType = file.file.ContentType;
                                   if (file.file.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.file.CopyTo(stream);
                                    
                                    
                                    
                                             file.FileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              }  
                             } 
                              if(faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile!=null)
                              {
                              faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileData = encoding.GetBytes( faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.Length.ToString());
                             faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileName = faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.FileName;
                             faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileType =faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.ContentType;
                              if (faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.Length > 0)
                            {
                                using (var stream = new MemoryStream())
                                 {
                                    faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceFile.CopyTo(stream);
                                    
                                    
                                    
                                    faatApplication.FaatAppResidenceInfo.ToList()[0].ResidenceInfoFileData = stream.ToArray();
                                    
                                 }
                             }
                              }
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0]!=null)
                              {
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder!=null)
                              {
                              foreach(var file in faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibJobHolder)
                              {   if(file.ContractFile!=null)
                              {
                                  file.ContractFileData =encoding.GetBytes( file.ContractFile.Length.ToString()); 
                                  file.ContractFileName = file.ContractFile.FileName;
                                  file.ContractFileType = file.ContractFile.ContentType;
                                   if (file.ContractFile.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.ContractFile.CopyTo(stream);
                                    
                                    
                                    
                                             file.ContractFileData = stream.ToArray();
                                    
                                        }
                                    }
                              }
                              }  
                              }
                              
                              if(faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent!=null)
                              { 

                               foreach(var file in faatApplication.FaatAppSiblingInfo.ToList()[0].FaatAppSibStudent)
                              {
                                  if(file.StudentCardFile!=null)
                                  {
                                  file.StdentCardFileData =encoding.GetBytes( file.StudentCardFile.Length.ToString()); 
                                  file.StdentCardFileName = file.StudentCardFile.FileName;
                                  file.StdentCardFileType = file.StudentCardFile.ContentType;
                                   if (file.StudentCardFile.Length > 0)
                                    {
                                       using (var stream = new MemoryStream())
                                        {
                                            file.StudentCardFile.CopyTo(stream);
                                    
                                    
                                    
                                             file.StdentCardFileData = stream.ToArray();
                                    
                                        }
                                    }
                                  }
                              }   
                                      
            }
            }
           
            }
            FaatIntakeSeason Intake = _context.FaatIntakeSeason.AsNoTracking().IncludeFilter(e=>e.FaatScholarshipStatus.Where(e=>e.Type=="Need Based")).OrderByDescending(e=>e.InsertionTimestamp).FirstOrDefault();

            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            
            faatApplication.InsertionTimestamp = DateTime.Now;
            //faatApplication.Status = "Pending";
            faatApplication.UpdateTimestamp = DateTime.Now;                
            
               

                FaatClassDefinition classDefinition = DbManager.CurrentNeedBasedStudent(Intake.Year+Intake.IntakeSeason,AridNo);
                FaatClassDefinition dbClass = _context.FaatClassDefinition.AsNoTracking().Where(e=>e.Discipline==classDefinition.Discipline && e.Section==classDefinition.Section && e.Semester==classDefinition.Semester && e.SemesterCount==classDefinition.SemesterCount).FirstOrDefault();
                if(dbClass!=null)
                {
                 faatApplication.CGPA = faatApplication.FaatAppStudentDetail.FirstOrDefault().Cpga;
                 faatApplication.AridNo = faatApplication.FaatAppStudentDetail.FirstOrDefault().AridNo;
                 faatApplication.Name = faatApplication.FaatAppStudentDetail.FirstOrDefault().Name;
                 faatApplication.ClassId =  dbClass.Id; 
                 faatApplication.status = ""; 

                _context.Add(faatApplication);
                _context.SaveChanges();
                }
                
                return RedirectToAction(nameof(UserProfile));
            }
            
            
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", faatApplication.UserId);
            //return View(faatApplication);
        

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            // var faatApplication = await _context.FaatApplication.FindAsync(id);
            // if (faatApplication == null)
            // {
            //     return NotFound();
            // }
            // ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", faatApplication.UserId);
            // return View(faatApplication);
            return NotFound();
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Status,InsertionTimestamp,AridNo,Name,Class,Section,UpdateTimestamp,ApplicationData")] FaatApplication faatApplication)
        {
            // if (id != faatApplication.Id)
            // {
            //     return NotFound();
            // }

            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _context.Update(faatApplication);
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!FaatApplicationExists(faatApplication.Id))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            //     return RedirectToAction(nameof(Index));
            // }
            // ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", faatApplication.UserId);
            // return View(faatApplication);
            return NotFound();
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }

            // var faatApplication = await _context.FaatApplication
            //     .Include(f => f.User)
            //     .FirstOrDefaultAsync(m => m.Id == id);
            // if (faatApplication == null)
            // {
            //     return NotFound();
            // }

            // return View(faatApplication);
             return NotFound();
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faatApplication = await _context.FaatApplication.FindAsync(id);
            _context.FaatApplication.Remove(faatApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaatApplicationExists(int id)
        {
            return _context.FaatApplication.Any(e => e.Id == id);
        }
        
        public IActionResult StudentDetails ()
        {
            var AridNo = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.Where(i => i.FaatScholarshipStatus.Any( s=>s.Status=="Pending" && s.Type=="Need Based")).FirstOrDefault();
            //FaatIntakeSeason faatIntakeSeason = _context.FaatIntakeSeason.IncludeFilter(a=>a.FaatScholarshipStatus.Where(e=>e.Status=="Pending" && e.Type=="Need Based")).FirstOrDefault();

            String PreviousSemester = "";
            String CurrentSemester = "";
               if(faatIntakeSeason == null)
            {
                // Set Error (Define Intake first) + Redirect to initial page
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
                }
            }                
                
            FaatAppStudentDetail _objModel= DbManager.StudentDetail(CurrentSemester,PreviousSemester,AridNo);
                
            return PartialView(_objModel);
        }
        public IActionResult ParentDetails ()
        {
                FaatAppParentDetail _objModel  = new FaatAppParentDetail();
                return PartialView(_objModel);

        }
          public IActionResult ProfileGuardianOtherIncome (string containerPrefix)
        {
             ViewData["ContainerPrefix"] = containerPrefix;
              FaatAppGuardianOtherIncomeResourceFiles files = new FaatAppGuardianOtherIncomeResourceFiles();
              return PartialView(files);     

        }
         public IActionResult ProfileMotherOtherIncome(string containerPrefix)
        {
             ViewData["ContainerPrefix"] = containerPrefix;
              FaatAppMotherOtherIncomeResourceFiles files = new FaatAppMotherOtherIncomeResourceFiles();
              return PartialView(files); 

        }
        public IActionResult GuardianOtherIncome (string containerPrefix)
        {
             ViewData["ContainerPrefix"] = containerPrefix;
              FaatAppGuardianOtherIncomeResourceFiles files = new FaatAppGuardianOtherIncomeResourceFiles();
              return PartialView(files);     

        }
        public IActionResult MotherOtherIncome(string containerPrefix)
        {
             ViewData["ContainerPrefix"] = containerPrefix;
              FaatAppMotherOtherIncomeResourceFiles files = new FaatAppMotherOtherIncomeResourceFiles();
              return PartialView(files); 

        }

        public IActionResult ResidenceInfo ()
        {
                FaatAppResidenceInfo _objModel  = new  FaatAppResidenceInfo();
                return PartialView(_objModel);

        }
         public IActionResult ProfileAddSibJobHolder (string containerPrefix)
        {       
                ViewData["ContainerPrefix"] = containerPrefix;
                FaatAppSibJobHolder _objModel  = new  FaatAppSibJobHolder();
                return PartialView(_objModel);

        }
          public IActionResult ProfileAddSibStudent (string containerPrefix)
        {      
                ViewData["ContainerPrefix"] = containerPrefix;
                FaatAppSibStudent _objModel  = new  FaatAppSibStudent();
                return PartialView(_objModel);

        }
         public IActionResult AddSibJobHolder (string containerPrefix,int JobCount)
        {       TempData["JobCount"]  = JobCount;
                ViewData["ContainerPrefix"] = containerPrefix;
                FaatAppSibJobHolder _objModel  = new  FaatAppSibJobHolder();
                return PartialView(_objModel);

        }
        public IActionResult AddSibStudent (string containerPrefix , int StudentCount)
        {       TempData["StudentCount"] = StudentCount;
                ViewData["ContainerPrefix"] = containerPrefix;
                FaatAppSibStudent _objModel  = new  FaatAppSibStudent();
                return PartialView(_objModel);

        }
        public IActionResult SiblingsInfo ()
        {
                FaatAppSiblingInfo _objModel  = new  FaatAppSiblingInfo ();
                return PartialView(_objModel);


        }
        
    }
}
