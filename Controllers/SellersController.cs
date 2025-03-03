﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;
using NewMVCProject.ViewModels;

namespace NewMVCProject.Controllers
{
    public class SellersController : Controller
    {
        private readonly MVCProjContext _context;

        public SellersController(MVCProjContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string strId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
            {
                return RedirectToAction("Login", "Authentication"); // redirect to login if session is invalid
            }

            var seller = await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == id);
            if (seller == null)
            {
                return NotFound();
            }
            else
            {
                return View(seller);
            }


        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (_context.Users.Any(u => u.Username == viewModel.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
            }
            if (ModelState.IsValid)
            {//create and save user
                var user = new User
                {

                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Username = viewModel.Username,
                    PasswordHash = AuthenticationController.HashPassword(viewModel.Password),
                    Property = "Seller"//assign seller role
                };
                _context.Users.Add(user);//save to db
                await _context.SaveChangesAsync();

                //create and save seller
                var seller = new Seller
                {
                    UserId = user.UserId
                };
                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Admins");
            }

            return View(viewModel);
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.SellerId == id);
        }
    }
}
