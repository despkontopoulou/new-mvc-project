using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;
using NewMVCProject.ViewModels;

namespace NewMVCProject.Controllers
{
    public class BillsController : Controller
    {
        private readonly MVCProjContext _context;

        public BillsController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            string phoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber)) 
            {
                return RedirectToAction("Login", "Authentication");
            }
            var bills = await _context.Bills
                .Where(b => b.PhoneNumber == phoneNumber)
                .Include(b => b.PhoneNumberNavigation)
                .ToListAsync();

            return View(bills);
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.PhoneNumberNavigation)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillViewModel viewModel)
        {
            var existingClient = await _context.Clients.AnyAsync(c => c.PhoneNumber == viewModel.PhoneNumber);
            if (!existingClient)
            {
                ModelState.AddModelError("PhoneNumber", "Client with this phone number does not exist.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var bill = new Bill
                {
                    PhoneNumber = viewModel.PhoneNumber,
                    Costs = viewModel.Costs
                };

                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sellers");
            }
            return View(viewModel);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.PhoneNumberNavigation)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
