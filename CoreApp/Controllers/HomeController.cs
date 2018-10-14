using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;

namespace CoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainContext _context;
        private IUserManager userManager;

        public HomeController(MainContext context, IUserManager userManager)
        {
            _context = context;
            this.userManager = userManager;

        }

  
        public IActionResult Index()
        {
            ViewBag.Tesla = _context.CredentialTypes.ToList();
            return View();
        }

        public IActionResult Test()
        {
           
            return View("Test");
        }

        [HttpPost]
        public IActionResult Login()
        {
            ValidateResult validateResult = this.userManager.Validate("Email", "admin@example.com", "admin");

            if (validateResult.Success)
                this.userManager.SignIn(this.HttpContext, validateResult.User, false);

            return this.RedirectToAction("Test");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            this.userManager.SignOut(this.HttpContext);
            return this.RedirectToAction("Test");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
