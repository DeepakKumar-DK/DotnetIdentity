using CustomerIdentity.Models;
using CustomerIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CustomerIdentity.Controllers
{
    public class AccountController:Controller
    {
        public readonly SignInManager<AppUser> signInManager;
        public readonly UserManager<AppUser> userManager;
        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            this.signInManager=signInManager;
            this.userManager = userManager;
        }
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginVM.Username!, loginVM.Password!, loginVM.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login Error");
            }
            return View(loginVM);
        }
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName= registerVM.Email,
                    Name= registerVM.Name,
                    Email= registerVM.Email,
                    Address= registerVM.Address
                };
                var result=await userManager.CreateAsync(appUser, registerVM.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach( var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }
    }
}
