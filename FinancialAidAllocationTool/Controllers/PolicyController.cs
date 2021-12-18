using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinancialAidAllocationTool.Models;
using FinancialAidAllocationTool.Models.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace FinancialAidAllocationTool.Controllers
{
    public class PolicyController : Controller
    {
        private readonly FaaToolDBContext _context;

        public PolicyController(FaaToolDBContext context)
        {
            _context = context;
        }

        // GET: Policy
        public async Task<IActionResult> Index()
        {
            return View(await _context.FaatPolicy.ToListAsync());
        }

        // GET: Policy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatPolicy = await _context.FaatPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faatPolicy == null)
            {
                return NotFound();
            }

            return View(faatPolicy);
        }

        // GET: Policy/Create
        public IActionResult Create()
        {
            
            FaatPolicy policy = _context.FaatPolicy
            .Include(policy => policy.FaatRule)
             .ThenInclude(rule => rule.FaatRuleDescription)
            .Single(p => p.Id == 1);
          
            //return View(new FaatPolicy());
            return View(policy);
        }
        [HttpGet]
        public IActionResult ManagePolicy()
        {
            
            List<FaatPolicy> AllPolicies = _context.FaatPolicy
            .Include(policy => policy.FaatRule)
             .ThenInclude(rule => rule.FaatRuleDescription).ToList();
            ;
            /*
             AllPolicies = _context.FaatPolicy
            .ToList();
            ;
          
            FaatPolicy a = new FaatPolicy();
            a.Id = 1;
            a.Name = "Default";
            a.IsSelected = 0;
            FaatPolicy b = new FaatPolicy();
            b.Id = 1;
            b.Name = "M. Haris";
            b.IsSelected = 1;

            AllPolicies = new List<FaatPolicy>();
            AllPolicies.Add(a);
            AllPolicies.Add(b);
            */
            //return View(new List<FaatPolicy>());
            return View(AllPolicies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Save( IEnumerable<FaatPolicy> faatPolicy)
        public async Task<IActionResult> ManagePolicy(List<FaatPolicy> faatPolicy)
        {
            if(ModelState.IsValid)
            {
                
                // _context.Update(faatPolicy);
                // await _context.SaveChangesAsync();
            _context.FaatRuleDescription.RemoveRange(_context.FaatRuleDescription);
            _context.FaatRule.RemoveRange(_context.FaatRule);
            _context.FaatPolicy.RemoveRange(_context.FaatPolicy);
          await _context.SaveChangesAsync();
            
            faatPolicy.RemoveAll(e=>e==null);
            foreach(var item in faatPolicy)
            {
               item.FaatRule = item.FaatRule.Where(e=>e!=null).ToList();
               foreach(var i  in item.FaatRule)
               {
                   i.FaatRuleDescription= i.FaatRuleDescription.Where(e=>e!=null).ToList();
               }

            }



           // faatPolicy.RemoveAll(e=>e==null);
            _context.AddRange(faatPolicy);
            await _context.SaveChangesAsync();
          
            
            
             return View(_context.FaatPolicy.Include(e=>e.FaatRule).ThenInclude(e=>e.FaatRuleDescription));

                //await Thread.Sleep(1000);
                /*
                _context.Add(faatPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                */
            }
            else
            {        
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList(); 
             faatPolicy.RemoveAll(e=>e==null);
            foreach(var item in faatPolicy)
            {
               item.FaatRule = item.FaatRule.Where(e=>e!=null).ToList();
               foreach(var i  in item.FaatRule)
               {
                   i.FaatRuleDescription= i.FaatRuleDescription.Where(e=>e!=null).ToList();
               }

            } 
                return View(faatPolicy);
  
            }
        }

        public IActionResult AddPolicy()
        {            
            return PartialView(new FaatPolicy());
        }
        public IActionResult AddRule(string containerPrefix)
        {            
            ViewData["ContainerPrefix"] = containerPrefix;            
            return PartialView(new FaatRule());
        }

        public IActionResult AddRuleDescription(string containerPrefix)
        {
            ViewData["ContainerPrefix"] = containerPrefix;            
            return PartialView(new FaatRuleDescription());            
        }

        // POST: Policy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsSelected")] FaatPolicy faatPolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faatPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faatPolicy);
        }

        // GET: Policy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatPolicy = await _context.FaatPolicy.FindAsync(id);
            if (faatPolicy == null)
            {
                return NotFound();
            }
            return View(faatPolicy);
        }

        // POST: Policy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsSelected")] FaatPolicy faatPolicy)
        {
            if (id != faatPolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faatPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaatPolicyExists(faatPolicy.Id))
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
            return View(faatPolicy);
        }

        // GET: Policy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faatPolicy = await _context.FaatPolicy
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (faatPolicy == null)
            {
                return NotFound();
            }

            return View(faatPolicy);
        }

        // POST: Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faatPolicy = await _context.FaatPolicy.FindAsync(id);
            _context.FaatPolicy.Remove(faatPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaatPolicyExists(int id)
        {
            return _context.FaatPolicy.Any(e => e.Id == id);
        }
    }
}
