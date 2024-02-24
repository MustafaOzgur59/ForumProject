using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public async Task addPostComment(int postId, string replyContent){
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var post = GetById(postId);
            var postReply = new PostReply
            {
                Content = replyContent,
                CreateTime = DateTime.Now,
                Post = post,
                User = user
            };
            post.PostReplies.Add(postReply);
            await _context.SaveChangesAsync();
            Console.WriteLine("post yorumu yapildi");
        }

        public IEnumerable<Post> GetAll()
        {
             return _context.Posts
                .Include(post => post.User)
                .Include(post => post.PostReplies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum);
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
            .Include(post => post.User)
            .Include(post => post.PostReplies)
                .ThenInclude(reply => reply.User)
            .Include(post => post.Forum)
            .First();
        }

        public Post GetByTitle(string title){
            return _context.Posts.Where(post => post.Title == title)
            .Include(post => post.User)
            .Include(post => post.PostReplies)
                .ThenInclude(reply => reply.User)
            .Include(post => post.Forum)
            .First();
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums.Where(forum => forum.Id == id).FirstOrDefault().Posts;
        }

        public IEnumerable<Post> GetLatestPosts(int v){
            return GetAll().OrderByDescending(p => p.CreateTime).Take(v).ToList();
        }

        public IEnumerable<Post> GetByUser(string id)
        {
            return _context.Posts.Where(post => post.User.Id == id)
            .Include(post => post.User)
            .Include(post => post.PostReplies)
                .ThenInclude(reply => reply.User)
            .Include(post => post.Forum)
            .ToList();
        }
    }

    public interface IPostService
    {
        Post GetById(int id);
        Post GetByTitle(string title);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByForum(int id);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        IEnumerable<Post> GetLatestPosts(int v);

        Task addPostComment(int postId, string replyContent);
        IEnumerable<Post> GetByUser(string id);
    }
}