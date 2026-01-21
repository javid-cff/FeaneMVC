using System.Threading.Tasks;
using FeaneMVC.Models.AppUser;
using FeaneMVC.ViewModels.AppUserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeaneMVC.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = new()
            {
                UserName = vm.Username,
                Fullname = vm.Fullname,
                Email = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemember, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"
            });

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });

            return Ok("Roles Created!");
        }
    }
}
