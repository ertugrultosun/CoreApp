using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        public IActionResult Index()
        {
            Users user = new Users();
            if (this.User.Identity.IsAuthenticated)
            {
                user = this.userManager.GetCurrentUser(HttpContext);
                return this.RedirectToAction("Main");
            }
            else
            {
                return this.View();
            }
            
            return this.View();
        }

        [Authorize]
        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            ValidateResult validateResult = this.userManager.Validate("Email", "admin@example.com", "admin");

            if (validateResult.Success)
                this.userManager.SignIn(this.HttpContext, validateResult.User, false);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            this.userManager.SignOut(this.HttpContext);
            return this.RedirectToAction("Index");
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
