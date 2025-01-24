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
    public class PhoneProgramsController : Controller
    {
        private readonly MVCProjContext _context;

        public PhoneProgramsController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: PhonePrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Programs.ToListAsync());
        }

        // GET: PhonePrograms/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneProgram = await _context.Programs
                .FirstOrDefaultAsync(m => m.ProgramName == id);
            if (phoneProgram == null)
            {
                return NotFound();
            }

            return View(phoneProgram);
        }

        // GET: PhonePrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhonePrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneProgramViewModel phoneProgram)
        {
            if (ModelState.IsValid)
            {
                var program = new PhoneProgram//create program
                {
                    ProgramName = phoneProgram.ProgramName,
                    Benefits = phoneProgram.Benefits,
                    Charge = phoneProgram.Charge
                };
                _context.Add(program);//save to db
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Admins");
            }
            return View(phoneProgram);
        }

        // GET: PhonePrograms/Edit/5
        public async Task<IActionResult> Edit(string programName)
        {
            if (string.IsNullOrEmpty(programName)) { 
                return NotFound();
            }

            var phoneProgram = await _context.Programs.FirstOrDefaultAsync(p => p.ProgramName == programName);
            
            if (phoneProgram == null)
            {
                return NotFound();
            }

            var phoneProgramViewModel = new PhoneProgramViewModel
            {
                ProgramName= phoneProgram.ProgramName,
                Benefits= phoneProgram.Benefits,
                Charge = phoneProgram.Charge

            };

            return View(phoneProgramViewModel);
        }

        // POST: PhonePrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string programName, PhoneProgramViewModel phoneProgramViewModel)
        {
            if (programName != phoneProgramViewModel.ProgramName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var phoneProgram = await _context.Programs.FirstOrDefaultAsync(p =>p.ProgramName == programName);
                    if (phoneProgram == null) {
                        return NotFound();
                    }

                    phoneProgram.Benefits = phoneProgramViewModel.Benefits;
                    phoneProgram.Charge = phoneProgramViewModel.Charge;

                    _context.Update(phoneProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneProgramExists(phoneProgramViewModel.ProgramName))
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
            return View(phoneProgramViewModel);
        }

        private bool PhoneProgramExists(string id)
        {
            return _context.Programs.Any(e => e.ProgramName == id);
        }
    }
}
