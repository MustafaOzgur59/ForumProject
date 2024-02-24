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
        private readonly ISearchService _searchService;

        public ForumController(IForumService forumService, ISearchService searchService, IPostService postService)
        {
            _forumService = forumService;
            _searchService = searchService;
            _postService = postService;
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

        [HttpPost]
        public IActionResult Search(int Id, string searchQuery){
            var forum = _forumService.GetById(Id);
            var searchResult =_searchService.getPostsInForumByQuery(Id,searchQuery).Select(post => new PostListModel
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
            }).ToList();
            var topicModel = new ForumTopicModel
            {
                Posts = searchResult,
                Forum = new ForumViewModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                }
            };
            return View("Topic", topicModel);
        }
    }
}