using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Entity;
using ForumProject.Models.Forum;
using ForumProject.Models.Post;
using ForumProject.Models.User;
using ForumProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(IUserService userService, UserManager<User> userManager, IPostService postService, IConfiguration configuration, IUploadService uploadService)
        {
            _userService = userService;
            _userManager = userManager;
            _postService = postService;
            _configuration = configuration;
            _uploadService = uploadService;
        }

        public IActionResult Detail(string id){
            var user = _userService.getById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var posts = _postService.GetByUser(id);
            var postListings = posts.Select(post => new PostListModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating.ToString(),
                Title = post.Title,
                DatePosted = post.CreateTime,
                ReplyCount = post.PostReplies.Count(),
                Forum = new ForumViewModel{
                    Id = post.Forum.Id,
                    Name = post.Forum.Title,
                    Description = post.Forum.Description,
                    ImageUrl = post.Forum.ImageUrl
                }
            });
            var model = new ProfileModel
            {
                UserId = user.Id,
                CreateDate = user.CreateDate.GetValueOrDefault(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                UserName = user.UserName,
                Posts = postListings,
                IsAdmin = userRoles.Contains("Admin")
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file){
            var userId = _userManager.GetUserId(User);
            var image_uri = await _uploadService.UploadImageAndGetUriAsync($"{userId}_{file.FileName}", file.OpenReadStream());
            await _userService.SetProfileImage(userId,new Uri(image_uri));
            return RedirectToAction("Detail","Profile", new {id = userId});
        }
    }
}