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
    public class PhonesController : Controller
    {
        private readonly MVCProjContext _context;

        public PhonesController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phones.ToListAsync());
        }

            private bool PhoneExists(string id)
        {
            return _context.Phones.Any(e => e.PhoneNumber == id);
        }
    }
}
