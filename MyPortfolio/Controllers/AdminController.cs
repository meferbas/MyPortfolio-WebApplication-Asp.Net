using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL;

namespace MyPortfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyPortfolioContext _context;
        private readonly EfSeedData _seedData; // Dependency injection

        public AdminController(MyPortfolioContext context, EfSeedData seedData) // Constructor
        {
            _context = context; // Injected via constructor
            _seedData = seedData; // Injected via constructor
        }

        [HttpGet]
        public IActionResult Index() // Login page
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nickname, string password) // Login post request
        {
            var adminUser = _context.Admins.FirstOrDefault(u => u.NickName == nickname); // Get the admin user from the database

            if (adminUser != null && _seedData.HashPassword(password) == adminUser.Password) // Check if the user exists and the password is correct
            {
                var claims = new List<Claim>    // Create the claims for the user
                {
                    new Claim(ClaimTypes.Name, adminUser.NickName), // Add the nickname
                    new Claim(ClaimTypes.Role, adminUser.Role) // Add the role
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); // Create the claims identity
                var authProperties = new AuthenticationProperties { IsPersistent = true }; // Create the authentication properties

                await HttpContext.SignInAsync("AdminCookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties); // Sign in the user

                return RedirectToAction("Index", "About"); // Redirect to the admin page
            }

            ModelState.AddModelError("", "Invalid login attempt. Please try again.");   // Add an error message
            return View();
        }
    }
}
