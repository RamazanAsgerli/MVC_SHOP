using Core.Models;
using Data.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC_SHOP.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View();
            User newUser = new User()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
            };
            var result = await _userManager.CreateAsync(newUser,registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username adi yanlisdir ve ya yoxdur");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.IsRemember, false);

            return RedirectToAction("index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role=new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role1 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);

        //    return Ok("Rollarrr Yarandiiiiiiiiiii");

        //}

        //public async Task<IActionResult> AddRole()
        //{
        //    var role = await _userManager.FindByNameAsync("ramazann");
        //    await _userManager.AddToRoleAsync(role, "SuperAdmin");
        //    return Ok("Rolll verildiiii!!!!!!!!!!!");
        //}

    }
}
