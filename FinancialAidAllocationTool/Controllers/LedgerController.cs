using System;
using Microsoft.AspNetCore.Mvc;
using FinancialAidAllocationTool.Models.Ledger;
using FinancialAidAllocationTool.Models;
using System.Linq;
namespace FinancialAidAllocationTool.Controllers
{
    public class LedgerController : Controller
    {

         private readonly FaaToolDBContext _context;

        public LedgerController(FaaToolDBContext context)
        {
                  _context = context;
        }
        //Without Pagination
        // public IActionResult DonationLedger()
        // {            
        //     return View(_context.DonationLedger.OrderByDescending(d => d.TransactionId).ToList());
        // }
        [HttpGet]
        public IActionResult DonationLedger(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,string id)
        {
            searchString = id;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.DonationLedger.OrderByDescending(d => d.TransactionId).ToList()
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                 students = students.Where(p => p.TransactionDate==DateTime.Parse(searchString)).ToList();
            }
            switch (sortOrder)
            {
                // case "name_desc":
                //     students = students.OrderByDescending(s => s.LastName);
                //     break;
                // case "Date":
                //     students = students.OrderBy(s => s.EnrollmentDate);
                //     break;
                // case "date_desc":
                //     students = students.OrderByDescending(s => s.EnrollmentDate);
                //     break;
                // default:
                //     students = students.OrderBy(s => s.LastName);
                //     break;
            }


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
            

            int pageSize = 3;
            return View(PaginatedList<DonationLedger>.Create(students.AsQueryable(), pageNumber ?? 1, pageSize));
        }

         [HttpPost]
          public IActionResult DonationLedger(float donations)
          {
                 var AddDonations = new DonationLedger();
            if(donations <= 0)
            {
                TempData["Error"]="Donation must be a non zero positive value.";        
                //return View("DonationLedger");
            }
            else
            {
                
                AddDonations.Credit = donations;
                AddDonations.TransactionDate = DateTime.Now;
                AddDonations.Debit = 0;
                AddDonations.Memo = "value of Donation is "+donations; 
                _context.DonationLedger.Add(AddDonations);
                _context.SaveChanges();                
            }
              return RedirectToAction("DonationLedger");
          }
        public IActionResult AddDonationLedger(float donations)
        {
            var AddDonations = new DonationLedger();
            if(donations <= 0)
            {
                TempData["Error"]="Donation must be a non zero positive value.";        
                //return View("DonationLedger");
            }
            else
            {
                
                AddDonations.Credit = donations;
                AddDonations.TransactionDate = DateTime.Now;
                AddDonations.Debit = 0;
                AddDonations.Memo = "value of Donation is "+donations; 
                _context.DonationLedger.Add(AddDonations);
                _context.SaveChanges();                
            }
            
            //return RedirectToAction (nameof(DonationLedger));
            return PartialView (AddDonations);
            
        }


    }
}