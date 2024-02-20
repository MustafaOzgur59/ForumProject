using ForumProject.Entity;
using ForumProject.Models.User;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(){
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model){
            if (ModelState.IsValid){
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded){
                    return RedirectToAction("Index","Forum");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            // Buraya kadar gelirse bir sikinti var demektir.
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

                [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Forum");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if (ModelState.IsValid)
            {
                Console.WriteLine(model.Password);
                var user = new User { UserName = model.UserName , Email = model.Email, };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded){
                    return RedirectToAction("Index", "Forum");
                }else {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                        ModelState.AddModelError("", $"Invalid register attempt: {error.Description}.");
                    }
                    return View(model);
                }  
            }
            ModelState.AddModelError("","Invalid register attempt2.");
            return View(model);
        }
    }
}