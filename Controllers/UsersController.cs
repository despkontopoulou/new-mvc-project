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
    public class UsersController : Controller
    {
        private readonly MVCProjContext _context;

        public UsersController(MVCProjContext context)
        {
            _context = context;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
