using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using FinancialAidAllocationTool.Models.Policy;
using FinancialAidAllocationTool.Models.Ledger;
using FinancialAidAllocationTool.Models.Application;
using FinancialAidAllocationTool.Models;
using FinancialAidAllocationTool.Models.IntakeSeason;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinancialAidAllocationTool.Models
{
  public class DBAccessLayer
  {
      private readonly IConfiguration config;
      
      private readonly FaaToolDBContext _context;

       
      public DBAccessLayer(IConfiguration config,FaaToolDBContext context)
      {
          _context = context;
          this.config = config;
      }
      
      public void SeasonDefinition()
      {
        FaatIntakeSeason season = _context.FaatIntakeSeason.Include(e=>e.FaatScholarshipStatus).Where(e=>e.FaatScholarshipStatus.Any(e=>e.Status=="Pending")).FirstOrDefault();
        List<FaatIntakeSeason> SeasonList =CurrPrev();
        List<FaatScholarshipStatus> scholarshipStatuses = new List<FaatScholarshipStatus>();
        //comparing current from BiitDbNew database
        if(season!=null)
        {        
       
            if(season.Year ==SeasonList.ElementAt(1).Year && season.IntakeSeason == SeasonList.ElementAt(1).IntakeSeason)
            {
                foreach(var item in season.FaatScholarshipStatus)
                {
                    if(item.Status == "Pending")
                    {
                        item.Status = "Complete";
                    }
                }
                _context.Update(season);
                _context.SaveChanges();
                FaatScholarshipStatus Merit = new FaatScholarshipStatus();
                Merit.Type = "Merit Based";
                Merit.Status = "Pending";
                FaatScholarshipStatus Need = new FaatScholarshipStatus();
                Need.Type = "Need Based";
                Need.Status = "Pending";
                scholarshipStatuses.Add(Merit);
                scholarshipStatuses.Add(Need);
                SeasonList[0].IntakeSeason = SeasonList[0].IntakeSeason.ToUpper();
                SeasonList[0].FaatScholarshipStatus = scholarshipStatuses;
                SeasonList[0].InsertionTimestamp = DateTime.Now;
                _context.Add(SeasonList[0]);
                _context.SaveChanges();

                   
            
             
        }
        }
        else
        {
            FaatIntakeSeason first = new FaatIntakeSeason();
            first.Year = SeasonList[0].Year;
            first.IntakeSeason = SeasonList[0].IntakeSeason.ToUpper();
            first.InsertionTimestamp = DateTime.Now;
                FaatScholarshipStatus Merit = new FaatScholarshipStatus();
                Merit.Type = "Merit Based";
                Merit.Status = "Pending";
                FaatScholarshipStatus Need = new FaatScholarshipStatus();
                Need.Type = "Need Based";
                Need.Status = "Pending";
                List <FaatScholarshipStatus> list = new List<FaatScholarshipStatus>();
                list.Add(Merit);
                list.Add(Need);
                first.FaatScholarshipStatus = list;
                
                _context.Add(first);
                _context.SaveChanges();


        }
      }
      public List<FaatIntakeSeason> CurrPrev()
      {
          string constring = config.GetConnectionString("ResultDBConnectionString");
          SqlConnection con = new SqlConnection (constring);
          con.Open();
          string query =string.Format(@"select top 2* from 
          (select DISTINCT  SUBSTRING(Semester_no,1,len(Semester_no)-2)as Year ,
          right(Semester_no,2) as season  from accgpa)as q 
          where q.season in ('FM','SM') ORDER by  q.Year desc ,q.season asc");
          SqlCommand cmd = new SqlCommand(query,con);
          SqlDataReader SDR = cmd.ExecuteReader();
          List<FaatIntakeSeason> SeasonList = new List<FaatIntakeSeason>();
                     while (SDR.Read())
                    { FaatIntakeSeason obj = new FaatIntakeSeason();
                       obj.Year = int.Parse(SDR[0].ToString());
                       obj.IntakeSeason = SDR[1].ToString(); 
                       SeasonList.Add(obj);

                    }
          return SeasonList;
      }
      public IEnumerable<Student> MeritList(string Discipline,
      string Semester,string TopStudents,float TotalAllocation)
      {
          List<Student> MeritList = new List<Student>();
          string constring = config.GetConnectionString("DefaultConnectionString");
          SqlConnection con = new SqlConnection (constring);
          con.Open();
          string query = "select distinct Top "+TopStudents.ToString()+" [dbo].Accgpa.REG_NO,[dbo].Accgpa.CGPA,[dbo].sectiontbl.discipline,[dbo].Accgpa.section,[dbo].Accgpa.semC,[dbo].Accgpa.SEMESTER_NO  from dbo.Accgpa,dbo.sectiontbl where  [dbo].sectiontbl.reg_no= [dbo].Accgpa.REG_NO and [dbo].sectiontbl.discipline='"+Discipline+"' and [dbo].Accgpa.SEMESTER_NO like'"+Semester+"' ORDER By CGPA DESC";
          SqlCommand cmd = new SqlCommand(query,con);
          SqlDataReader SDR = cmd.ExecuteReader();
                          while (SDR.Read())

                          {
                              Student obj = new Student();
                              obj.RegisterNo = SDR[0].ToString();
                              obj.CGPA = float.Parse((SDR[1].ToString()));
                              obj.Discipline = SDR[2].ToString();
                              obj.Section = SDR[3].ToString();
                              obj.Semester = SDR[4].ToString();
                              obj.AllocationAmount = TotalAllocation/float.Parse(TopStudents.ToString());
                              MeritList.Add(obj);
                            }
                              con.Close();                       
          return MeritList;
          
      }
      
      public void Settings(String Students, String SemesterNo,float Budget,float Allocation)
      {
          string constring = config.GetConnectionString("DefaultConnectionString");
          SqlConnection con = new SqlConnection (constring);
          con.Open();
          string query = "insert into settings values ('"+SemesterNo+"','"+Students+"','"+Budget.ToString()+"','"+Allocation.ToString()+"')";
          SqlCommand cmd = new SqlCommand(query,con);
          cmd.ExecuteNonQuery();
          con.Close();

      }
        
                    
     public List<ClassDetails> PrepareScholarListModifiedVersion(List<ClassDetails> StudentResultList,
      FaatPolicy policy)
      {
           List<ClassDetails> ScholarList = new List<ClassDetails>();
           List<ClassDetails> FinalScholars = new List<ClassDetails>();
           int i=0;
           while(i<policy.FaatRule.Count())
           {
              
              ScholarList= StudentResultList.Where(e=> policy.FaatRule.Count()-i==1 ? e.ClassStrength<policy.FaatRule.ToList()[i].Strength : e.ClassStrength>policy.FaatRule.ToList()[i].Strength)
              .AsEnumerable().Select(e=> new ClassDetails 
              { ClassStrength=e.ClassStrength,Discipline=e.Discipline,
              Section=e.Section,SemesterCount=e.SemesterCount,
              SemesterNo=e.SemesterNo , StudentList= e.StudentList
              .Where(e=>e.CGPA>=policy.MeritMinCGPA).Take(policy.FaatRule.
              ToList()[i].Top).ToList()}).ToList(); 
              StudentResultList  = StudentResultList
              .Where(e=> !ScholarList
              .Any(y=>y.ClassStrength==e.ClassStrength && 
              y.Discipline==e.Discipline && y.Section == 
              e.Section && y.SemesterCount==e.SemesterCount
               && y.SemesterNo == e.SemesterNo)).ToList();
              foreach(var item in ScholarList)
              { int j=0;
                  foreach(var std in item.StudentList)
                  {

                      std.AllocationAmount = policy.FaatRule.ToList()[i].FaatRuleDescription.ToList()[j].Amount;
                      j++;
                  }
              } 
              i++; 
              FinalScholars.AddRange(ScholarList);
           }
          foreach(var item in FinalScholars)
          {
              List<Student> list = new List<Student>();
              var groups = item.StudentList.GroupBy(e=>e.CGPA).ToList();
              foreach(var l in groups)
              {
                  foreach(var std in l)
                  {
                      double amount = l.Average(e=>e.AllocationAmount);
                      Student obj = new Student() ;
                      obj.AllocationAmount = amount;
                      obj.CGPA = std.CGPA;
                      obj.Name = std.Name;
                      obj.RegisterNo = std.RegisterNo;
                      list.Add(obj);    

                  }

                  
              }
              item.StudentList.Clear();
              item.StudentList=list;

          }   



           return FinalScholars;

           
      }

      public List<ClassDetails> PrepareScholarList(List<ClassDetails> StudentResultList,
      FaatPolicy policies)
      {
          List<ClassDetails> ScholarList = new List<ClassDetails>();
          foreach(var stdResultDetail in StudentResultList)
          {
              ClassDetails obj = new ClassDetails();
              obj.Discipline = stdResultDetail.Discipline;
              obj.SemesterNo = stdResultDetail.SemesterNo;
              obj.SemesterCount = stdResultDetail.SemesterCount;
              obj.Section = stdResultDetail.Section;              
              obj.ClassStrength = stdResultDetail.ClassStrength;
                    
              foreach(var pDesc in policies.FaatRule)
              {
                  if(stdResultDetail.ClassStrength >= pDesc.Strength)
                  {
                      int i= 0;
                      while(i<pDesc.Top)
                      {
                          bool flag = false;
                          for(int j=i+1; j<pDesc.Top; j++)
                          {
                              flag = true;
                              if(stdResultDetail.StudentList[i].CGPA
                              == stdResultDetail.StudentList[j].CGPA)
                              {

                                  if(j + 1 != pDesc.Top)
                                  {
                                    continue;
                                  }
                                  else
                                  {

                                      double amount = 0;
                                      for(int k=i; k<=j;  k++)
                                      {
                                          amount = amount + pDesc.FaatRuleDescription.ToList()[k].Amount;
                                      }
                                      amount = amount/(j-i+1);
                                      for(int k=i; k<=j; k++)
                                      {
                                          Student StudnetObj  = new Student();     
                                          StudnetObj.RegisterNo = stdResultDetail.StudentList[k].RegisterNo;
                                          StudnetObj.Name = stdResultDetail.StudentList[k].Name;
                                          StudnetObj.CGPA = stdResultDetail.StudentList[k].CGPA;
                                          StudnetObj.AllocationAmount=amount;
                                          if(StudnetObj.CGPA>= policies.MeritMinCGPA)
                                          {
                                          obj.StudentList.Add(StudnetObj);
                                          }        
                                      }
                                      i = j + 1;
                                      break;  
                                  }
                              }
                              else
                              {
                                  double amount = 0;
                                  for(int k=i; k<j && i+1 != j; k++)
                                  {
                                      amount = amount + pDesc.FaatRuleDescription.ToList()[k].Amount;
                                  }
                                  if(amount != 0)
                                  {
                                      amount = amount/(j-i);
                                      for(int k=i; k<j; k++)
                                      {
                                          Student StudnetObj  = new Student();     
                                          StudnetObj.RegisterNo = stdResultDetail.StudentList[k].RegisterNo;
                                          StudnetObj.Name = stdResultDetail.StudentList[k].Name;
                                          StudnetObj.CGPA = stdResultDetail.StudentList[k].CGPA;
                                          StudnetObj.AllocationAmount=amount;
                                          if(StudnetObj.CGPA>=policies.MeritMinCGPA)
                                          {
                                          obj.StudentList.Add(StudnetObj);
                                          }        
                                      }
                                  }
                                  else
                                  {
                                      amount = pDesc.FaatRuleDescription.ToList()[i].Amount;

                                      Student StudnetObj  = new Student();     
                                      StudnetObj.RegisterNo = stdResultDetail.StudentList[i].RegisterNo;
                                      StudnetObj.Name = stdResultDetail.StudentList[i].Name;
                                      StudnetObj.CGPA = stdResultDetail.StudentList[i].CGPA;
                                      StudnetObj.AllocationAmount=amount;
                                      if(StudnetObj.CGPA>=policies.MeritMinCGPA)
                                      {
                                      obj.StudentList.Add(StudnetObj);
                                      }        
                                  }                                  

                                  i=j;
                                  break;
                              }
                              
                          }
                          if(flag == false)
                          {
                            double amount = pDesc.FaatRuleDescription.ToList()[i].Amount;

                            Student StudnetObj  = new Student();     
                            StudnetObj.RegisterNo = stdResultDetail.StudentList[i].RegisterNo;
                            StudnetObj.Name = stdResultDetail.StudentList[i].Name;
                            StudnetObj.CGPA = stdResultDetail.StudentList[i].CGPA;
                            StudnetObj.AllocationAmount=amount;
                            if(StudnetObj.CGPA>=policies.MeritMinCGPA)
                            {
                            obj.StudentList.Add(StudnetObj);        
                            }
                            i = i + 1;                              
                          }
                          
                          
                      }
                      //Pick the top and break policy loop
                      //Allocate the funds
                      break;
                  }              

              }
              ScholarList.Add(obj);
          }
          return ScholarList;
      }

      public List<FaatScholarLog> RemainingMeritStudentList(String PreviousSemester,String CurrentSemester, double? cgpa, string [] ClassDesc)
      { 
          List<ClassDetails> ResultsOfAllStudents = new List<ClassDetails>();
        List<FaatScholarLog> AllClassStudents = new List<FaatScholarLog>();
        string discipline = ClassDesc[0];
        string semC = ClassDesc[1];
        string section = ClassDesc[2];  
        var ClassId = _context.FaatClassDefinition.Where(e=>e.Section==section && e.SemesterCount==semC && e.Discipline==discipline && e.Semester == CurrentSemester).Select(e=>e.Id).FirstOrDefault();
        string constring = config.GetConnectionString("ResultDBConnectionString");
        SqlConnection con = new SqlConnection (constring);
        con.Open();
//


 string query =string.Format(@"select distinct a.reg_no,a.Name,p.cgpa from                        
                                        (select distinct 
                                        [dbo].STMTR.Final_course as disp,
                                        upper([dbo].Accgpa.semester_no) as semester_no,
                                        [dbo].Accgpa.semC as Semc,
                                        upper([dbo].STMTR.Section) as section, 
                                        [dbo].STMTR.reg_no as reg_no,[dbo].STMTR.Enrl_status,([dbo].STMTR.St_firstname+' '+[dbo].STMTR.[St_lastname]) as [Name],
                                        [dbo].[AccGPA].cgpa                            
                                        from [dbo].Accgpa,[dbo].STMTR 
                                        where   
                                        [dbo].Accgpa.semester_no='{0}'
                                        and [dbo].STMTR.Final_course = '{1}'
                                        and [dbo].Accgpa.semC = '{2}'
                                        and [dbo].STMTR.Section = '{3}'
                                        
                                        and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                                        and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no)as a,
                                        (select Reg_No as reg_no,CGPA as cgpa from Accgpa where SEMESTER_NO = '{5}' and cgpa>='{6}')as p
                                        where a.Reg_No = p.Reg_No
                                        ",CurrentSemester,discipline,semC,section,cgpa,PreviousSemester,cgpa);      
        SqlCommand cmd = new SqlCommand(query,con);
        SqlDataReader SDR = cmd.ExecuteReader();

       
      if(SDR.HasRows)
      {

        while(SDR.Read())
        { 
            FaatScholarLog obj = new FaatScholarLog();
            obj.ClassId = int.Parse(ClassId.ToString());
            //obj.  = SDR[1].ToString();
            //obj.SemesterCount = SDR[2].ToString();
           // obj.Section = SDR[3].ToString();
            obj.AridNo = SDR[0].ToString();
            obj.Name = SDR[1].ToString();
            obj.Cgpa = float.Parse( SDR[2].ToString());
            
            AllClassStudents.Add(obj);
            
        }
      } 
        cmd.Cancel();
        con.Close();
        SDR.Close();

        string constring1 = config.GetConnectionString("ResultDBConnectionString");
        SqlConnection con1 = new SqlConnection (constring1);
        con1.Open();
        string query1 = string.Format(@"
                            select distinct a.reg_no,a.Name,a.cgpa
                             from                        
                            (select distinct 
                            [dbo].STMTR.Final_course as disp,
                            upper([dbo].Accgpa.semester_no) as semester_no,
                            [dbo].Accgpa.semC as Semc,
                            upper([dbo].STMTR.Section) as section, 
                            [dbo].STMTR.reg_no as reg_no,[dbo].STMTR.Enrl_status,([dbo].STMTR.St_firstname+' '+[dbo].STMTR.[St_lastname]) as [Name],
                             [dbo].[AccGPA].cgpa                            
                            from [dbo].Accgpa,[dbo].STMTR 
                            where   
                            [dbo].Accgpa.semester_no='{0}'
                            and [dbo].STMTR.Final_course = '{1}'
                            and [dbo].Accgpa.semC = '{2}'
                            and [dbo].STMTR.Section = '{3}'
                            

                            and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                            and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no)as a where a.Reg_No not in
                            (select distinct [dbo].STMTR.Reg_No  from [dbo].Accgpa,[dbo].STMTR  where 
                            [dbo].Accgpa.semester_no='{4}'
                            and [dbo].STMTR.Final_course = '{5}'
                            and [dbo].Accgpa.semC = '{6}'
                            and [dbo].STMTR.Section = '{7}'
                            

                            and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                            and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no
                            ) and a.CGPA >='{8}'
            
                            ",CurrentSemester,discipline,semC,section,PreviousSemester,discipline,int.Parse(semC)-1,section,cgpa);

         SqlCommand cmd1 = new SqlCommand(query1,con1);
        SqlDataReader SDR1 = cmd1.ExecuteReader();
             while (SDR1.Read())
             {
                FaatScholarLog obj = new FaatScholarLog();
                obj.ClassId = int.Parse(ClassId.ToString());
         
            obj.AridNo = SDR1[0].ToString();
             obj.Name = SDR1[1].ToString();
            obj.Cgpa = float.Parse( SDR1[2].ToString());
           
            AllClassStudents.Add(obj);
             }
             SDR1.Close();
             cmd1.Cancel();
             con1.Close();
              string constring3 = config.GetConnectionString("ResultDBConnectionString");
              SqlConnection con3 = new SqlConnection (constring3);
              con3.Open();
             string query3 = String.Format(@"  select AridNo,Name,CGPA
                                from [FaaToolDB].[dbo].FAAT_ScholarLog sch, [FaaToolDB].[dbo].Faat_Class_Definition cl
                                where type = 'Merit Based'
                                and cl.Discipline = '{0}'
                                and cl.Semester = '{1}'
                                and cl.SemesterCount = '{2}'
                                and cl.Section = '{3}'
                                and sch.Class_ID = cl.id",discipline,CurrentSemester,semC,section);


        SqlCommand cmd3 = new SqlCommand(query3,con3);
        SqlDataReader SDR3 = cmd3.ExecuteReader();
               
        List<FaatScholarLog> ScholarList = new List<FaatScholarLog>();
        while(SDR3.Read())
        {
            FaatScholarLog StudnetObj  = new FaatScholarLog(); 
            StudnetObj.ClassId = int.Parse(ClassId.ToString());    
            StudnetObj.AridNo = SDR3[0].ToString();
            StudnetObj.Name = SDR3[1].ToString();
            StudnetObj.Cgpa = float.Parse(SDR3[2].ToString());
            ScholarList.Add(StudnetObj);
        }
        
        SDR3.Close();
        con3.Close();
        cmd3.Cancel();

        List<FaatScholarLog> StudentList  = new List<FaatScholarLog>();
        StudentList = AllClassStudents.Where(e=> !ScholarList.Any(y=>y.AridNo==e.AridNo)).ToList(); 

        return StudentList;
      }  

      public List<ClassDetails> StudentResultList(String CurrentSemester , String PreviousSemester)
      {        
        string constring = config.GetConnectionString("ResultDBConnectionString");
        SqlConnection con = new SqlConnection (constring);
        con.Open();
        string query =string.Format(@"   select a.disp,a.semester_no,a.Semc,a.section,a.reg_no,a.Enrl_status,a.Name,p.cgpa from                        
                                        (select distinct 
                                        [dbo].STMTR.Final_course as disp,
                                        upper([dbo].Accgpa.semester_no) as semester_no,
                                        [dbo].Accgpa.semC as Semc,
                                        upper([dbo].STMTR.Section) as section, 
                                        [dbo].STMTR.reg_no as reg_no,[dbo].STMTR.Enrl_status,([dbo].STMTR.St_firstname+' '+[dbo].STMTR.[St_lastname]) as [Name],
                                        [dbo].[AccGPA].cgpa                            
                                        from [dbo].Accgpa,[dbo].STMTR 
                                        where   
                                        [dbo].Accgpa.semester_no='{0}'
                                        and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                                        and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no and [dbo].Accgpa.SemC>1)as a,
                                        (select Reg_No as reg_no,CGPA as cgpa from Accgpa where SEMESTER_NO = '{1}')as p
                                        where a.Reg_No = p.Reg_No
                                        order by 1 , 2, 3, 4, 8 desc",CurrentSemester,PreviousSemester);      
        SqlCommand cmd = new SqlCommand(query,con);
        SqlDataReader SDR = cmd.ExecuteReader();

       
        List<ClassDetails> ResultsOfAllStudents = new List<ClassDetails>();
        List<Student> StudentList = new List<Student>();
        while(SDR.Read())
        { 
            Student obj = new Student();
            obj.Discipline = SDR[0].ToString();
            obj.Semester  = SDR[1].ToString();
            obj.SemesterCount = SDR[2].ToString();
            obj.Section = SDR[3].ToString();
            obj.RegisterNo = SDR[4].ToString();
            obj.CGPA = float.Parse( SDR[7].ToString());
            obj.Name = SDR[6].ToString();
            StudentList.Add(obj);
            
  
        }

  
        SDR.Close();
        con.Close();
        cmd.Cancel();
        string constring1 = config.GetConnectionString("ResultDBConnectionString");
        SqlConnection con1 = new SqlConnection (constring1);
        con1.Open();

         string query1 = string.Format(@"
                            select distinct a.disp,a.semester_no,a.Semc,a.section,a.reg_no,a.Enrl_status,a.Name,a.cgpa
                             from                        
                            (select distinct 
                            [dbo].STMTR.Final_course as disp,
                            upper([dbo].Accgpa.semester_no) as semester_no,
                            [dbo].Accgpa.semC as Semc,
                            upper([dbo].STMTR.Section) as section, 
                            [dbo].STMTR.reg_no as reg_no,[dbo].STMTR.Enrl_status,([dbo].STMTR.St_firstname+' '+[dbo].STMTR.[St_lastname]) as [Name],
                             [dbo].[AccGPA].cgpa                            
                            from [dbo].Accgpa,[dbo].STMTR 
                            where   
                            [dbo].Accgpa.semester_no='{0}'
                            and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                            and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no and [dbo].Accgpa.SemC>1)as a where a.Reg_No not in
                            (select distinct Reg_No  from Accgpa where SEMESTER_NO = '{1}')
            
                            order by 1 , 2, 3, 4, 8 desc ",CurrentSemester,PreviousSemester);

         SqlCommand cmd1 = new SqlCommand(query1,con1);
        SqlDataReader SDR1 = cmd1.ExecuteReader();
             while (SDR1.Read())
             {
                Student obj = new Student();
            obj.Discipline = SDR1[0].ToString();
            obj.Semester  = SDR1[1].ToString();
            obj.SemesterCount = SDR1[2].ToString();
            obj.Section = SDR1[3].ToString();
            obj.RegisterNo = SDR1[4].ToString();
            obj.CGPA = float.Parse( SDR1[7].ToString());
            obj.Name = SDR1[6].ToString();
            StudentList.Add(obj);
             }

         List<ClassDetails> Classes= StudentList.GroupBy(e=>(e.Semester,e.SemesterCount,e.Section,e.Discipline)).AsEnumerable().Select(e=>new ClassDetails{Discipline=e.FirstOrDefault().Discipline,Section=e.FirstOrDefault().Section,SemesterNo = e.FirstOrDefault().Semester,SemesterCount = e.FirstOrDefault().SemesterCount,StudentList=e.ToList(),ClassStrength = e.Count()}).ToList();

        return Classes;
      }

      public void StudentLog(String SemesterNo,String RegistrationNo,String Submit,float AllocationAmount,String Discipline,String SemesterCount,String Section,String Type)

      {
          if(Submit.Equals("Accept"))
          {

              
          }
          if(Submit.Equals("Reject"))
          {
                 
		}
        
        
      }
      public void Budget(float Amount)
      {
        string constring = config.GetConnectionString("DefaultConnectionString");
        SqlConnection con = new SqlConnection (constring);
        con.Open();
        string query="insert into Budget(TransactionDate,Amount) values ('"+DateTime.Now.ToString()+"','"+Amount+"')";
        SqlCommand cmd1 = new SqlCommand(query,con);
        cmd1.ExecuteNonQuery();

      }
      public void CompleteList( string[] AridNo,String Semester,float[] AllocationAmount,string Status,string Type)
      {
              for(int i =0 ; i<AridNo.Length;i++)
              {
    
              }
        

      }

     
       public void RegisterUser(RegisterView model,FaaToolDBContext _cotext)
       {
           Users obj = new Users();
           obj.AridNo =  model.AridNo;
           obj.Password  = model.Password;
           obj.Role  = "Student";
           obj.Name = FindName(model.AridNo);
           _cotext.Add(obj);
           _cotext.SaveChanges();
       } 
       public bool FindUser(String Arid)
       {
          string constring = config.GetConnectionString("ResultDBConnectionString");
          SqlConnection con = new SqlConnection (constring);
          con.Open();
           string query = "select Reg_No from STMTR where Enrl_status='ENROLLED' and Reg_No = '"+Arid+"'";
           SqlCommand cmd = new SqlCommand(query,con);
           SqlDataReader sdr = cmd.ExecuteReader();
           if(sdr.HasRows)
           {
                return true;
           }
           else
           {
               return false;
           }


           
       }
       public String FatherName(String AridNo)
       {
           string constring = config.GetConnectionString("ResultDBConnectionString");
          SqlConnection con = new SqlConnection (constring);
           con.Open();
           string query = "select Father_name from STMTR where Enrl_status='ENROLLED' and Reg_No = '"+AridNo+"'";
           SqlCommand cmd = new SqlCommand(query,con);
           SqlDataReader sdr = cmd.ExecuteReader();
           String Name = "";
           while(sdr.Read())
           {
               Name= sdr[0].ToString();
           }
           return Name;
          
           
       }
       public String FindName (String AridNo)
       {
          string constring = config.GetConnectionString("ResultDBConnectionString");
          SqlConnection con = new SqlConnection (constring);
           con.Open();
           string query = "select St_firstname+' '+St_middlename+' '+St_lastname as Name from STMTR where Enrl_status='ENROLLED' and Reg_No = '"+AridNo+"'";
           SqlCommand cmd = new SqlCommand(query,con);
           SqlDataReader sdr = cmd.ExecuteReader();
           String Name = "";
           while(sdr.Read())
           {
               Name= sdr[0].ToString();
           }
           return Name;
           
       }
       public bool IsStudentEnrolled(string Semester_no,String AridNo)
       {
           string constring = config.GetConnectionString("ResultDBConnectionString");
           SqlConnection con = new SqlConnection (constring);
           con.Open();
           string query = String.Format(@"select top 1* from dbo.Accgpa where SEMESTER_NO = '{0}' and REG_NO='{1}' Order By SemC desc",Semester_no,AridNo);
           SqlCommand cmd = new SqlCommand(query,con);
           SqlDataReader sdr = cmd.ExecuteReader();
           if(sdr.HasRows)
           {
               con.Close();
               return true;
           }
           else
           {
               con.Close();
               return false;
           }
           

       }
        public List<ClassDetails> CurrentSemesterStudents(String Semster)
        {        
            string constring = config.GetConnectionString("ResultDBConnectionString");
            SqlConnection con = new SqlConnection (constring);
            con.Open();
            
            String query =  String.Format(
                            @"select distinct 
                            [dbo].STMTR.Final_course,
                            upper([dbo].Accgpa.semester_no) as semester_no,
                            [dbo].Accgpa.semC,
                            upper([dbo].Accgpa.section) as section, 
                            [dbo].STMTR.reg_no,[dbo].STMTR.Enrl_status,([dbo].STMTR.St_firstname+' '+[dbo].STMTR.[St_lastname]) as [Name],
                            [dbo].Accgpa.CGPA   
                            from Accgpa,STMTR 
                            where   
                            [dbo].Accgpa.semester_no='{0}'
                            
                            and lower(trim([dbo].STMTR.Enrl_status)) = 'enrolled'
                            and [dbo].STMTR.Reg_No=[dbo].Accgpa.reg_no 
                            order by 1 , 2, 3, 4, 8 desc",Semster);
                    
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataReader SDR = cmd.ExecuteReader();

            String Prev_Discipline = "";
            String Prev_SemisterNo = "";
            String Prev_SemisterCount = "";
            String Prev_SectionA = "";

            String Curr_Discipline = "";
            String Curr_SemisterNo = "";
            String Curr_SemisterCount = "";
            String Curr_SectionA = "";
            
            int ClassStrength = 0; 
            List<ClassDetails> ResultsOfAllStudents = new List<ClassDetails>();
            List<Student> StudentList = new List<Student>();
            while(SDR.Read())
            {
                Curr_Discipline = SDR[0].ToString();
                Curr_SemisterNo = SDR[1].ToString();
                Curr_SemisterCount = SDR[2].ToString();
                Curr_SectionA = SDR[3].ToString();

                
                if(Prev_Discipline == Curr_Discipline
                    && Prev_SemisterNo == Curr_SemisterNo
                    && Prev_SemisterCount == Curr_SemisterCount
                    && Prev_SectionA == Curr_SectionA)
                {
                    //Student of Same Class
                    ClassStrength++;

                    Student StudnetObj  = new Student();     
                    StudnetObj.RegisterNo = SDR[4].ToString();
                    StudnetObj.Name = SDR[6].ToString();
                    StudnetObj.CGPA = float.Parse(SDR[7].ToString());
                    StudnetObj.AllocationAmount=0;
                    StudentList.Add(StudnetObj);
                }
                else
                {                
                    //Class Changed
                    //Student of New Class
                    if(Prev_Discipline != ""
                        && Prev_SemisterNo != ""
                        && Prev_SemisterCount != ""
                        && Prev_SectionA != "") 
                    {
                        ClassDetails obj = new ClassDetails();
                        obj.Discipline = Prev_Discipline;
                        obj.SemesterNo = Prev_SemisterNo;
                        obj.SemesterCount = Prev_SemisterCount;
                        obj.Section = Prev_SectionA;
                        
                        obj.ClassStrength = ClassStrength;
                        
                        obj.StudentList = new List<Student>(StudentList);

                        ResultsOfAllStudents.Add(obj);

                        StudentList  = new List<Student>();
                    }

                    Student StudnetObj  = new Student();     
                    StudnetObj.RegisterNo = SDR[4].ToString();
                    StudnetObj.Name = SDR[6].ToString();
                    StudnetObj.CGPA = float.Parse(SDR[7].ToString());
                    StudnetObj.AllocationAmount=0;
                    StudentList.Add(StudnetObj);
                    
                    ClassStrength = 1;
                }

                Prev_Discipline = Curr_Discipline;
                Prev_SemisterNo = Curr_SemisterNo;
                Prev_SemisterCount = Curr_SemisterCount;
                Prev_SectionA = Curr_SectionA;

            }

            ClassDetails obj1 = new ClassDetails();
            obj1.Discipline = Prev_Discipline;
            obj1.SemesterNo = Prev_SemisterNo;
            obj1.SemesterCount = Prev_SemisterCount;
            obj1.Section = Prev_SectionA;
            
            obj1.ClassStrength = ClassStrength;
            
            obj1.StudentList = new List<Student>(StudentList);

            ResultsOfAllStudents.Add(obj1);

            SDR.Close();
            con.Close();
            cmd.Cancel();
            return ResultsOfAllStudents;
    }
     
     public FaatClassDefinition CurrentNeedBasedStudent (String Semester,String AridNo)
     {
         string constring = config.GetConnectionString("ResultDBConnectionString");
         SqlConnection con = new SqlConnection (constring);
         con.Open();
         string query = String.Format(@"select [dbo].STMTR.Final_course,
                                        dbo.Accgpa.SEMESTER_NO,dbo.Accgpa.SemC,
                                        dbo.Stmtr.SECTION from Accgpa,
                                        STMTR where dbo.Accgpa.REG_NO= dbo.STMTR.Reg_No 
                                        and dbo.Accgpa.SEMESTER_NO='{0}' 
                                        and dbo.Accgpa.REG_NO='{1}'",Semester,AridNo);
        SqlCommand cmd = new SqlCommand(query,con);
        SqlDataReader SDR = cmd.ExecuteReader();
        FaatClassDefinition ClassDefination = new FaatClassDefinition();

        while (SDR.Read())
        {
            ClassDefination.Discipline = SDR[0].ToString();
            ClassDefination.Semester = SDR[1].ToString();
            ClassDefination.SemesterCount = SDR[2].ToString();
            ClassDefination.Section = SDR[3].ToString();

        }
        con.Close();
        return ClassDefination;



     }
     public FaatAppStudentDetail StudentDetail(String CurrentSemester,String PreviousSemester,String AridNo)
     {
         string constring = config.GetConnectionString("ResultDBConnectionString");
         SqlConnection con = new SqlConnection (constring);
         con.Open();
         string query = String.Format(@"select [dbo].STMTR.Final_course,dbo.STMTR.ST_firstname+' '+dbo.STMTR.St_middlename+' '+dbo.STMTR.ST_LastName as [Name],
                                        dbo.Accgpa.SEMESTER_NO,dbo.Accgpa.SemC,
                                        dbo.STMTR.SECTION,dbo.stmtr.Em_telNo from Accgpa,
                                        STMTR where dbo.Accgpa.REG_NO= dbo.STMTR.Reg_No 
                                        and dbo.Accgpa.SEMESTER_NO='{0}' 
                                        and dbo.Accgpa.REG_NO='{1}'",CurrentSemester,AridNo);
        SqlCommand cmd = new SqlCommand(query,con);
        SqlDataReader SDR = cmd.ExecuteReader();
        FaatAppStudentDetail studentDetail = new FaatAppStudentDetail();
        while(SDR.Read())
        {
            studentDetail.Class=SDR[0].ToString()+SDR[3].ToString();
            studentDetail.Section=SDR[4].ToString();
            studentDetail.Name=SDR[1].ToString();
            studentDetail.MobileNo =SDR[5].ToString();
            studentDetail.AridNo = AridNo;
            studentDetail.Semester = CurrentSemester;


        }
        con.Close();
         string constring1 = config.GetConnectionString("ResultDBConnectionString");
         SqlConnection con1 = new SqlConnection (constring1);
         con1.Open();
         string query1 = String.Format(@"select [dbo].Accgpa.CGPA from dbo.Accgpa where SEMESTER_NO='{0}' 
                                        and dbo.Accgpa.REG_NO='{1}'",PreviousSemester,AridNo);
        SqlCommand cmd1 = new SqlCommand(query1,con1);
        SqlDataReader SDR1 = cmd1.ExecuteReader();
        while(SDR1.Read())
        {
         studentDetail.Cpga = float.Parse(SDR1[0].ToString());
        }
        return studentDetail;


     }
        
                                        

  }

}
