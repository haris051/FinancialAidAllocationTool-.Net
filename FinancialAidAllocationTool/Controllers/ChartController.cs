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
using Microsoft.Data.SqlClient;

namespace FinancialAidAllocationTool.Controllers
{
    public class ChartController : Controller
    {
        private readonly FaaToolDBContext _context;
        private readonly IConfiguration configuration;
        public DBAccessLayer DbManager;

        public ChartController(FaaToolDBContext context,IConfiguration configuration)
        {
            this.configuration = configuration;
            
            _context = context;
            DbManager = new DBAccessLayer(this.configuration,_context);
        }

        public IActionResult Index()
        {               
            
            string constring = configuration.GetConnectionString("FaaToolDB");
            SqlConnection con = new SqlConnection (constring);
            con.Open();
            
            String query =  String.Format(
                @"select 'Merit Based' Type, case when sum(dd.Allocation_Amount) is null then 0 else sum(dd.Allocation_Amount)end amount
                from 
                (
                    select case when Intake_Season = 'FM' then concat([Year], 'SM') else  concat([Year]-1, 'FM')end Semester
                    from Faat_Scholarship_Status aa, Faat_Intake_Season bb
                    where aa.status = 'Pending'
                    and aa.[Type] = 'Merit Based'
                    and aa.Intake_Season_ID = bb.Id    
                )bb,
                Faat_Class_Definition cc, FAAT_ScholarLog dd
                where dd.[Status] = 'Accept'
                and dd.[Type] = 'Merit Based'
                and cc.Semester = bb.Semester
                and cc.id = dd.Class_ID
                UNION
                select 'Need Based' Type, case when sum(dd.Allocation_Amount) is null then 0 else sum(dd.Allocation_Amount)end amount
                from Faat_Scholarship_Status aa, Faat_Intake_Season bb, 
                Faat_Class_Definition cc, FAAT_ScholarLog dd
                where aa.status = 'Pending'
                and aa.[Type] = 'Need Based'
                and dd.[Status] = 'Accept'
                and dd.[Type] = 'Need Based'
                and aa.Intake_Season_ID = bb.Id
                and cc.Semester = CONCAT(bb.[Year],bb.Intake_Season)
                and cc.id = dd.Class_ID;");
    
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataReader SDR = cmd.ExecuteReader();

            while(SDR.Read())
            {
                if(SDR[0].ToString() == "Merit Based")
                {
                    ViewData["Merit Based"] = SDR[1].ToString();       
                }
                if(SDR[0].ToString() == "Need Based")
                {
                    ViewData["Need Based"] = SDR[1].ToString();       
                }                
            }        
    
            con.Close();
            return View();
        }
        
    }
}
