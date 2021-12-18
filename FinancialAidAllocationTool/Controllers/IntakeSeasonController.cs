using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models;
using FinancialAidAllocationTool.Models.IntakeSeason;

namespace FinancialAidAllocationTool.Controllers
{
    public class IntakeSeasonController : Controller
    {
        private readonly FaaToolDBContext _context;

        public IntakeSeasonController(FaaToolDBContext context)
        {
            _context = context;
        }

        // GET: IntakeSeason
        public async Task<IActionResult> index()
        {
            return View(await _context.FaatIntakeSeason.ToListAsync());
        }

        // GET: IntakeSeason/Details/5
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

        // GET: IntakeSeason/Create
        public IActionResult Create()
        {
            FaatIntakeSeason intakeSeason = new FaatIntakeSeason();
            intakeSeason.Year = DateTime.Now.Year;
            return View(intakeSeason);
        }

        // POST: IntakeSeason/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,IntakeSeason,InsertionTimestamp")] FaatIntakeSeason faatIntakeSeason)
        {
            if (ModelState.IsValid)
            {
                faatIntakeSeason.InsertionTimestamp = DateTime.Now;

                FaatIntakeSeason PrevIntakeSeason = _context.FaatIntakeSeason.ToList().OrderByDescending(a => a.InsertionTimestamp).FirstOrDefault();
                FaatScholarshipStatus PendingScholarshipStatus = _context.FaatScholarshipStatus.Where(a => a.Status == "Pending").FirstOrDefault();

                if(PendingScholarshipStatus == null)
                {
                    //Sucess
                    if(faatIntakeSeason.Year >= PrevIntakeSeason.Year)
                    {
                        if(faatIntakeSeason.Year == PrevIntakeSeason.Year)
                        {
                            if(PrevIntakeSeason.IntakeSeason == "FM" 
                                && faatIntakeSeason.IntakeSeason == "FM")
                            {
                                TempData["error"] = "Fall semister is already complete.";
                                return View(faatIntakeSeason);        
                            }
                            else if(PrevIntakeSeason.IntakeSeason == "FM" 
                                && faatIntakeSeason.IntakeSeason == "SM")
                            {
                                TempData["error"] = "Spring cannot come after Fall in same year.";
                                return View(faatIntakeSeason);        
                            }
                            else if(PrevIntakeSeason.IntakeSeason == "SM" 
                                && faatIntakeSeason.IntakeSeason == "FM")
                            {
                                //1. Sucess
                                //When input year is equal and Fall is coming ater spring
                                

                                using (var transaction = _context.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        FaatScholarshipStatus MeritBased = new FaatScholarshipStatus();                                    
                                        MeritBased.Type = "Merit Based";
                                        MeritBased.Status = "Pending";
                                        
                                        faatIntakeSeason.FaatScholarshipStatus.Add(MeritBased);
                                                                            
                                        //string tmp = "0";
                                        //int x = 2 / int.Parse(tmp);

                                        FaatScholarshipStatus NeedBased = new FaatScholarshipStatus(); 
                                        NeedBased.Type = "Need Based";
                                        NeedBased.Status = "Pending";

                                        faatIntakeSeason.FaatScholarshipStatus.Add(NeedBased);

                                        _context.Add(faatIntakeSeason);
                                        _context.SaveChanges();

                                        // Commit transaction if all commands succeed, transaction will auto-rollback
                                        // when disposed if either commands fails
                                        transaction.Commit();

                                    }
                                    catch (Exception)
                                    {
                                        transaction.Rollback();
                                        // TODO: Handle failure

                                        TempData["error"] = "An exception has occured and rollback is perfomed.";
                                        return View(faatIntakeSeason);        
                                    }
                                }
                                TempData["error"] = "";
                                return RedirectToAction(nameof(Index));
                            }
                            else if(PrevIntakeSeason.IntakeSeason == "SM" 
                                && faatIntakeSeason.IntakeSeason == "SM")
                            {
                                TempData["error"] = "Spring semister is already complete.";
                                return View(faatIntakeSeason);        
                            }
                            else
                            {
                                TempData["error"] = "Failed to determine the defined semister.";
                                return View(faatIntakeSeason);
                            }

                        }
                        else
                        {
                            
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                try
                                {   
                                    FaatScholarshipStatus MeritBased = new FaatScholarshipStatus();                                    
                                    MeritBased.Type = "Merit Based";
                                    MeritBased.Status = "Pending";
                                    
                                    faatIntakeSeason.FaatScholarshipStatus.Add(MeritBased);
                                                                        
                                    //string tmp = "0";
                                    //int x = 2 / int.Parse(tmp);

                                    FaatScholarshipStatus NeedBased = new FaatScholarshipStatus(); 
                                    NeedBased.Type = "Need Based";
                                    NeedBased.Status = "Pending";

                                    faatIntakeSeason.FaatScholarshipStatus.Add(NeedBased);

                                    _context.Add(faatIntakeSeason);
                                    _context.SaveChanges();

                                    // Commit transaction if all commands succeed, transaction will auto-rollback
                                    // when disposed if either commands fails
                                    transaction.Commit();
                            
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    // TODO: Handle failure

                                    TempData["error"] = "An exception has occured and rollback is perfomed.";
                                    return View(faatIntakeSeason);        
                                }
                            }
                            TempData["error"] = "";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        TempData["error"] = "The system time may be wrong.";
                        return View(faatIntakeSeason);
                    }
                }
                else
                {
                    TempData["error"] = "Please complete the pending Semister scholarship process.";
                    return View(faatIntakeSeason);
                }
            }

        
        
        
            return View(faatIntakeSeason);
        }

        // GET: IntakeSeason/Edit/5
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

        // POST: IntakeSeason/Edit/5
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

        // GET: IntakeSeason/Delete/5
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

        // POST: IntakeSeason/Delete/5
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
