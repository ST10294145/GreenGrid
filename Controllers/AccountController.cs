using GreenGrid.Data;
using GreenGrid.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GreenGrid.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(user.Email);
                if (userExist != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(user);
                }

                var createUserResult = await _userManager.CreateAsync(user, password);
                if (createUserResult.Succeeded)
                {
                    // Automatically login the user after registration (optional)
                    await _userManager.AddToRoleAsync(user, "Farmer");  // Assign role to user
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Get the user's roles
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Farmer"))
                {
                    HttpContext.Session.SetString("UserRole", "Farmer");
                }
                else if (roles.Contains("Employee"))
                {
                    HttpContext.Session.SetString("UserRole", "Employee");
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

    }
}
