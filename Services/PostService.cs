using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
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

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _context.Forums.Where(forum => forum.Id == id).FirstOrDefault().Posts;
        }

        public IEnumerable<Post> GetLatestPosts(int v){
            return GetAll().OrderByDescending(p => p.CreateTime).Take(v).ToList();
        }
    }

    public interface IPostService
    {
        Post GetById(int id);
        Post GetByTitle(string title);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<Post> GetPostsByForum(int id);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        IEnumerable<Post> GetLatestPosts(int v);
    }
}