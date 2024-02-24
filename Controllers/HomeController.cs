using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ForumProject.Models;
using ForumProject.Models.Home;
using ForumProject.Services;
using ForumProject.Models.Post;

namespace ForumProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostService _postService;
    private readonly ISearchService _searchService;

    public HomeController(ILogger<HomeController> logger, IPostService postService, ISearchService searchService)
    {
        _logger = logger;
        _postService = postService;
        _searchService = searchService;
    }

    public IActionResult Index()
    {
        var latestPosts = _postService.GetAll().OrderByDescending(p => p.CreateTime).Take(10).Select(p => new PostListModel {
            Id = p.Id,
            Title = p.Title,
            AuthorId = p.User.Id,
            AuthorName = p.User.UserName,
            DatePosted = p.CreateTime,
            ReplyCount = p.PostReplies.Count(),
            Forum = new Models.Forum.ForumViewModel {
                Id = p.Forum.Id,
                Description = p.Forum.Description,
                Name = p.Forum.Title,
                ImageUrl = p.Forum.ImageUrl
            }
        }).ToList();
        var model = new HomeIndexModel
        {
            LatestPosts = latestPosts,
            SearchQuery = ""
        };
        return View(model);
    }

    public IActionResult Search(string SearchQuery){
        if (SearchQuery == null)
        {
            return RedirectToAction("Index");
        }
        var searchResult =_searchService.getPostsByQuery(SearchQuery).Select(p => new PostListModel {
            Id = p.Id,
            Title = p.Title,
            AuthorId = p.User.Id,
            AuthorName = p.User.UserName,
            DatePosted = p.CreateTime,
            ReplyCount = p.PostReplies.Count(),
            Forum = new Models.Forum.ForumViewModel {
                Id = p.Forum.Id,
                Description = p.Forum.Description,
                Name = p.Forum.Title,
                ImageUrl = p.Forum.ImageUrl
            }
        }).ToList();
        var model = new HomeIndexModel
        {
            LatestPosts = searchResult,
            SearchQuery = ""
        };
        return View("Index",model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
