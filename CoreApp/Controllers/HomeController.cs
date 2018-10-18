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
using CoreApp.ViewModels.Home;

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
                return View("Landing");
            }
        }


        [Authorize]
        [Route("")]
        public IActionResult Main()
        {
            Users user = new Users();
            user = this.userManager.GetCurrentUser(HttpContext);
            MainViewModel main = new MainViewModel();
            return this.View(main);
        }

        [HttpPost]
        public IActionResult Login(string Identifier, string Secret)
        {
            //ValidateResult validateResult = this.userManager.Validate("Email", "admin@example.com", "admin");
            ValidateResult validateResult = this.userManager.Validate("Email", Identifier, Secret);
            if (validateResult.Success)
                this.userManager.SignIn(this.HttpContext, validateResult.User, false);
            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Register(string Identifier, string Secret, string Name)
        {
            if (_context.Credentials.Where(m=> m.Identifier == Identifier) == null)
            {
                userManager.SignUp(Name, "Email", Identifier, Secret);
                Login(Identifier, Secret);
            }
            return this.RedirectToAction("Index");
        }
        
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
