using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;
using NewMVCProject.ViewModels;
namespace NewMVCProject.Controllers
{
    public class ClientsController : Controller
    {
        private readonly MVCProjContext _context;

        public ClientsController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            string strId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
            {
                return RedirectToAction("Login", "Authentication"); // redirect to login if session is invalid
            }

            var client = await _context.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == id);

            HttpContext.Session.SetString("PhoneNumber", client.PhoneNumber);
            if (client == null) {
                return NotFound();
            }
            else
            {
                return View(client);
            }
           

        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhoneNumberNavigation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            string strId= HttpContext.Session.GetString("UserId");//id of seller
            if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id)) {
                return RedirectToAction("Login", "Authentication");
            }

            var phonePrograms = _context.Programs.Select(p => new SelectListItem
            {
                Text = p.ProgramName,
                Value = p.ProgramName
            }).ToList();

            ViewData["PhonePrograms"] = phonePrograms;

            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            

            string strId=HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id)) {
                return RedirectToAction("Login", "Authentication");
            }

            var phonePrograms = _context.Programs.Select(p => new SelectListItem
            {
                Text = p.ProgramName,
                Value = p.ProgramName
            }).ToList();

            ViewData["PhonePrograms"] = phonePrograms;

            var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == clientViewModel.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "This username is already taken.");
            }

            var existingPhone = await _context.Phones
            .FirstOrDefaultAsync(p => p.PhoneNumber == clientViewModel.PhoneNumber);

            if (existingPhone != null)
            {
                ModelState.AddModelError("PhoneNumber", "This phone number is already taken.");
            }

            if (ModelState.IsValid)
            {//user creation
                var user = new User
                {
                    FirstName = clientViewModel.FirstName,
                    LastName = clientViewModel.LastName,
                    Username = clientViewModel.Username,
                    PasswordHash = AuthenticationController.HashPassword(clientViewModel.Password),
                    Property = "Client"
                };
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                //phone creation
                var phone = new Phone
                {
                    PhoneNumber = clientViewModel.PhoneNumber,
                    ProgramName = clientViewModel.ProgramName
                };
                _context.Phones.Add(phone);
                await _context.SaveChangesAsync();

                var client = new Client
                {
                    Afm = clientViewModel.AFM,
                    PhoneNumber = phone.PhoneNumber,
                    UserId = user.UserId
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Sellers");
            }
            return View(clientViewModel);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return NotFound();
            }
            var phone=await _context.Phones
                .FirstOrDefaultAsync(p=>p.PhoneNumber==phoneNumber);
            if (phone == null) { 
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(c=>c.PhoneNumber ==phoneNumber);
            if (client == null)
            {
                return NotFound();
            }

            ViewData["Programs"] = await _context.Programs
                .Select(p => new SelectListItem
                {
                    Value = p.ProgramName,
                    Text = p.ProgramName
                })
                .ToListAsync();



            var clientProgramViewModel = new ClientProgramViewModel
            {
                PhoneNumber = client.PhoneNumber,
                ProgramName = phone.ProgramName
            };

            return View(clientProgramViewModel);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string phoneNumber,ClientProgramViewModel viewModel)
        {
            if (phoneNumber != viewModel.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var phone = await _context.Phones.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
                    if (phone == null) {
                        return NotFound();
                    }
                    phone.ProgramName = viewModel.ProgramName;

                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Sellers");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! _context.Phones.Any(p=>p.PhoneNumber==phoneNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
           return View(viewModel);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.PhoneNumberNavigation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        public async Task<IActionResult> SelectClient()
        {
            var clients = await _context.Clients
                .Include(c => c.User)
                .Select(c => new ClientListViewModel
                {
                    PhoneNumber = c.PhoneNumber,
                    FullName = c.User.FirstName + " " + c.User.LastName
                })
                .ToListAsync();

            return View(clients);
        }
    }
}
