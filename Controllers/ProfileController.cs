using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Entity;
using ForumProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
    
        public ProfileController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
    }
}