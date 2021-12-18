using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models.IntakeSeason;
using FinancialAidAllocationTool.Models;
using System.ComponentModel.DataAnnotations;

namespace FinancialAidAllocationTool
{
    public class CloseScholarshipController : Controller
    {
        private readonly FaaToolDBContext _context;
        
        public CloseScholarshipController(FaaToolDBContext context)
        {   

            _context = context;
        }

        // GET: CloseScholarship
       
        public async Task<IActionResult> Index(String Type)

        {
           
          
           TempData["Type"]=Type;
            var statuses = new List<FaatScholarshipStatus>();
            var IntakeSeasons = new List<FaatIntakeSeason>();
            //var FaatScholarshipStatus =await _context.FaatScholarshipStatus.Where(e=>e.Type=="Merit Based").ToListAsync();
            //var IntakeSeason = await _context.FaatIntakeSeason.ToListAsync();
           
            var Seasons = await _context.FaatIntakeSeason.Include(e=>e.FaatScholarshipStatus).OrderBy(e=>e.InsertionTimestamp).ToListAsync();
            
            var count=0;
            foreach(var item in Seasons)
            {
                FaatIntakeSeason Object = new FaatIntakeSeason();
                //Deep Copy
                Object.Id = item.Id;
                Object.InsertionTimestamp = item.InsertionTimestamp;
                Object.IntakeSeason = item.IntakeSeason;
                Object.Year = item.Year;

                //Shallow Copy [Address of a Memory location]   
                //Object.FaatScholarshipStatus = item.FaatScholarshipStatus;
                
                IntakeSeasons.Add(Object);
                
                //Shallow Copy makes a single location null.
                //Hence all pointers to the location will be accessing null
                //IntakeSeasons[count].FaatScholarshipStatus = null;

                foreach(var obj in item.FaatScholarshipStatus)
                {
                    if(obj.Type=="Merit Based" && Type=="Merit Based")
                    {
                        FaatScholarshipStatus ScholarshipStatusObject = new FaatScholarshipStatus();
                        ScholarshipStatusObject=obj;
                        IntakeSeasons[count].FaatScholarshipStatus.Add(ScholarshipStatusObject);
                    }
                    else if(obj.Type=="Need Based" && Type=="Need Based")
                    {
                        FaatScholarshipStatus ScholarshipStatusObject = new FaatScholarshipStatus();
                        ScholarshipStatusObject=obj;
                        IntakeSeasons[count].FaatScholarshipStatus.Add(ScholarshipStatusObject);
                    }                    
                }
                count++;
            }
   

            return View(IntakeSeasons);
            }
         
        [HttpPost]
       
        public IActionResult ChangeStatus (int ID,String Status,String Type)
        {
            var PendingApplications = _context.FaatScholarLog.Where(e=>e.Status == "Pending" && e.Type==Type).Count();

            if(PendingApplications > 0 && Type =="Need Based")
            {   
                TempData["Error"]="Please Complete the Pending Application before closing";

                
                
              //  var validation = new ValidationResult("Please Complete the Pending Application before closing");
               // TempData["Error"] = "";
                return RedirectToAction("Index",new{Type=Type});
            }
            else if(PendingApplications > 0 && Type == "Merit Based")
            {
                TempData["Error"]="Please Complete the Pending Application before closing";
                return RedirectToAction("Index",new{Type=Type});
            }
           
            
            var FaatScholarshipStatus = _context.FaatScholarshipStatus.Where(x=>x.Id==ID).FirstOrDefault();
            FaatScholarshipStatus.Status=Status;
            _context.Entry( _context.FaatScholarshipStatus.FirstOrDefaultAsync(x => x.Id == ID)).CurrentValues.SetValues(FaatScholarshipStatus);
            _context.SaveChangesAsync();
            return RedirectToAction("Index",new{Type=Type});
            
        }

        // GET: CloseScholarship/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatIntakeSeason = await _context.FaatIntakeSeason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faatIntakeSeason == null)
            {
                return NotFound();
            }

            return View(faatIntakeSeason);
        }

        // GET: CloseScholarship/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CloseScholarship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,IntakeSeason,InsertionTimestamp")] FaatIntakeSeason faatIntakeSeason,string Type)
        {
            TempData["Type"]=Type;
            if (ModelState.IsValid)
            {
                _context.Add(faatIntakeSeason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faatIntakeSeason);
        }

        // GET: CloseScholarship/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatIntakeSeason = await _context.FaatIntakeSeason.FindAsync(id);
            if (faatIntakeSeason == null)
            {
                return NotFound();
            }
            return View(faatIntakeSeason);
        }

        // POST: CloseScholarship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,IntakeSeason,InsertionTimestamp")] FaatIntakeSeason faatIntakeSeason)
        {
            if (id != faatIntakeSeason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faatIntakeSeason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaatIntakeSeasonExists(faatIntakeSeason.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faatIntakeSeason);
        }

        // GET: CloseScholarship/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatIntakeSeason = await _context.FaatIntakeSeason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faatIntakeSeason == null)
            {
                return NotFound();
            }

            return View(faatIntakeSeason);
        }

        // POST: CloseScholarship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faatIntakeSeason = await _context.FaatIntakeSeason.FindAsync(id);
            _context.FaatIntakeSeason.Remove(faatIntakeSeason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaatIntakeSeasonExists(int id)
        {
            return _context.FaatIntakeSeason.Any(e => e.Id == id);
        }
    }
}
