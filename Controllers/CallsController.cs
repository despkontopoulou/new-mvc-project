using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;

namespace NewMVCProject.Controllers
{
    public class CallsController : Controller
    {
        private readonly MVCProjContext _context;

        public CallsController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: Calls
        public async Task<IActionResult> Index()
        {
            string phoneNumber = HttpContext.Session.GetString("PhoneNumber");

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return RedirectToAction("Login", "Authentication"); 
            }

           var calls = _context.Calls
             .Where(c => c.PhoneNumber == phoneNumber) // Filters calls by phone number linked to bills
             .ToList();

            return View(calls); // Pass the ViewModel to the view
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        private bool CallExists(int id)
        {
            return _context.Calls.Any(e => e.CallId == id);
        }
    }
}
