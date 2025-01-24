using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;

namespace NewMVCProject.Controllers
{
    public class AdminsController : Controller
    {
        private readonly MVCProjContext _context;

        public AdminsController(MVCProjContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            string strId = HttpContext.Session.GetString("UserId");


            if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
            {
                return RedirectToAction("Login", "Authentication"); // redirect to login if session is invalid
            }


            var admin = await _context.Admins
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.UserId ==id);


            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                return View(admin);
            }

        }

       

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
