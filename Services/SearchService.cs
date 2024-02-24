using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public SearchService(IPostService postService, ApplicationDbContext context)
        {
            _postService = postService;
            _context = context;
        }

        public List<Post> getPostsByQuery(string searchQuery)
        {
            var posts = _postService.GetAll();

            var filteredPosts = posts.Where(
                    p => p.Title.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase)
                    || p.Content.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase)
                ).ToList();
            return filteredPosts;
        }

        public List<Post> getPostsInForumByQuery(int forumId, string searchQuery)
        {
            var forum = _context.Forums.Where(forum => forum.Id == forumId).First();
            return forum.Posts.Where(
                    p => p.Title.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase)
                    || p.Content.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase)
                ).ToList();

        }
    }

    public interface ISearchService{
        List<Post> getPostsByQuery(string searchQuery);
        List<Post> getPostsInForumByQuery(int forumId,string searchQuery); 
    }
}