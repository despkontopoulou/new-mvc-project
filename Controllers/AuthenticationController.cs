using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewMVCProject.Models;
using System.Threading.Tasks;
namespace NewMVCProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly MVCProjContext _context;
        public AuthenticationController(MVCProjContext context)
        {
            _context = context;
        }


        //GET:Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //Post:Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                TempData["LoginMessage"] = "Invalid username";
                return RedirectToAction("Login", "Authentication");
            }

            bool isPasswordValid = VerifyPassword(password, user.PasswordHash);

            if (isPasswordValid)
            {
                // Successful Login
                HttpContext.Session.SetString("UserId", user.UserId.ToString());//set session info
                HttpContext.Session.SetString("UserRole", user.Property);
                var routeValues = new RouteValueDictionary {
                    { "id", user.UserId }
                };
                //TempData["LoginMessage"] = "Successful Login";
                return RedirectToAction("Index", $"{user.Property}s", routeValues);
              
            }

            TempData["LoginMessage"] = "Invalid credentials";
            return RedirectToAction("Login", "Authentication");

        }
        public static bool VerifyPassword(string password, string passwordHash)
        {
            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }
    }

}

