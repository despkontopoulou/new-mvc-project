using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewMVCProject.Models;

namespace NewMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RedirectToRole()
        {
            string role= HttpContext.Session.GetString("UserRole");
            if (string.IsNullOrEmpty(role)){
                return RedirectToAction("Login", "Authentication");
            }
            return role switch
            {
                "Admin" => RedirectToAction("Index", "Admins"),
                "Client" => RedirectToAction("Index", "Clients"),
                "Seller" => RedirectToAction("Index", "Sellers"),
                _ => RedirectToAction("Login", "Authentication"),
            };
        }
    }
}
