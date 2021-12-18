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

namespace FinancialAidAllocationTool.Controllers
{
    public class NeedBasedController : Controller
    {
        private readonly FaaToolDBContext _context;
         public NeedBasedController(FaaToolDBContext context)
        {
            
            _context = context;
        }
       

    }
}