using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Services
{
    public class ForumService : IForumService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ForumService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(Forum forum)
        {
            await _applicationDbContext.Forums.AddAsync(forum);
            await _applicationDbContext.SaveChangesAsync();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _applicationDbContext.Forums
            .Include(forum => forum.Posts);
        }

        public IEnumerable<User> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            return _applicationDbContext.Forums.Where(forum => forum.Id == id)
            .Include(forum => forum.Posts)
                .ThenInclude(post => post.User)
            .Include(forum => forum.Posts)
                .ThenInclude(post => post.PostReplies)
                    .ThenInclude(reply => reply.User)
            .FirstOrDefault();
        }

        public Task UpdateForum(Forum forum)
        {
            throw new NotImplementedException();
        }
    }

    public interface IForumService
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        IEnumerable<User> GetAllActiveUsers();

        Task Create(Forum forum);

        Task Delete(int forumId);
        Task UpdateForum(Forum forum);

    }
}