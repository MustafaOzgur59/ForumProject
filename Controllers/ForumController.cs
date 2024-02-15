using ForumProject.Entity;
using ForumProject.Models.Forum;
using ForumProject.Models.Post;
using ForumProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForumProject.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IPostService _postService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index(){
            var forums = _forumService.GetAll()
            .Select(forum => new ForumViewModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
            });

            var forumIndex = new ForumIndexModel { ForumList = forums };
            return View(forumIndex);
        }

        public IActionResult Topic(int id){
            var forum = _forumService.GetById(id);
            var posts = forum.Posts;
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
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                }
            });

            var topicModel = new ForumTopicModel
            {
                Posts = postListings,
                Forum = new ForumViewModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                }
            };
            return View(topicModel);
        }
    }
}