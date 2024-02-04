using SocksWebsite.Data;
using SocksWebsite.Data.Static;
using SocksWebsite.Data.ViewModels;
using SocksWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SocksWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login() => View(new LoginM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginM loginM)
        {
            if (!ModelState.IsValid) return View(loginM);

            var user = await _userManager.FindByEmailAsync(loginM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginM);
        }


        public IActionResult Register() => View(new RegisterM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterM registerM)
        {
            if (!ModelState.IsValid) return View(registerM);

            var user = await _userManager.FindByEmailAsync(registerM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerM.FullName,
                Email = registerM.EmailAddress,
                UserName = registerM.EmailAddress
            };

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                // The role doesn't exist, you may want to create it.
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var newUserResponse = await _userManager.CreateAsync(newUser, registerM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}