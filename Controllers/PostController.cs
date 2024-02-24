using ForumProject.Entity;
using ForumProject.Models.Post;
using ForumProject.Models.Reply;
using ForumProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class PostController:Controller
    {
        private readonly IPostService _postService;
        private readonly IForumService _forumService;
        private readonly UserManager<User> _userManager;

        public PostController(IPostService postService, IForumService forumService, UserManager<User> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            PostIndexModel model = constructPostIndexModel(id);
            return View(model);
        }

        public IActionResult Create(int id){
            var forum = _forumService.GetById(id);
            var model = new CreatePostModel
            {
                ForumId = forum.Id,
                ForumName = forum.Title
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostModel model){
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            var post = new Post {
                User = user,
                Title = model.Title,
                Content = model.Content,
                CreateTime = DateTime.Now,
                Forum = _forumService.GetById(model.ForumId)

            };
            await _postService.Add(post);
            return RedirectToAction("Index","Post", new {id = post.Id});
        }

        public async Task<IActionResult> addComment(int postId,string replyContent){

            await _postService.addPostComment(postId, replyContent);
            return RedirectToAction("Index","Post", new {id = postId});
        }

        private PostIndexModel constructPostIndexModel(int id)
        {
            var post = _postService.GetById(id);
            var user = post.User;
            var replies = post.PostReplies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = (int)reply.User.Rating,
                Content = reply.Content,
                CreatedAt = reply.CreateTime,
                PostId = post.Id
            });
            
            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = user.UserName!,
                AuthorImageUrl = post.User.ProfileImageUrl!,
                AuthorRating = (int)post.User.Rating!,
                CreatedAt = post.CreateTime,
                Content = post.Content,
                Replies = replies,
                ForumName = post.Forum.Title,
                ForumId = post.Forum.Id
            };
            return model;
        }
    }
}