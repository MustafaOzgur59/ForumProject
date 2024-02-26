using ForumProject.Entity;
using ForumProject.Models.User;
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
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model){
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
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
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
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
                        ModelState.AddModelError("", $"Invalid register attempt: {error.Description}.");
                    }
                    return View(model);
                }  
            }
            return View(model);
        }
    }
}