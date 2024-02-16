using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.Post;
using ForumProject.Models.Reply;
using ForumProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class PostController:Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int id){
            var post = _postService.GetById(id);
            var replies = post.PostReplies.Select(reply => new PostReplyModel {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Content = reply.Content,
                CreatedAt = reply.CreateTime,
                PostId = post.Id
            });
            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                CreatedAt = post.CreateTime,
                Content = post.Content,
                Replies = replies
            };
            return View(post);
        }
    }
}