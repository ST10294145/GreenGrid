using GreenGrid.Data;
using GreenGrid.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GreenGrid.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password, string role)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Attempting to register user: {user.Email} with role: {role}");

                var userExist = await _userManager.FindByEmailAsync(user.Email);
                if (userExist != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(user);
                }

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created successfully");

                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                        _logger.LogInformation($"Created new role: {role}");
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, role);

                    if (roleResult.Succeeded)
                    {
                        _logger.LogInformation($"Added user to role: {role}");
                        return RedirectToAction("Login");
                    }

                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        _logger.LogError($"Role error: {error.Description}");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    _logger.LogError($"User creation error: {error.Description}");
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
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
            _logger.LogWarning($"Failed login attempt for email: {email}");
            return View();
        }
    }
}