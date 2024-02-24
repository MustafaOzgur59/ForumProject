using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace ForumProject.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> getAll()
        {
            return _context.Users;
        }

        public User getById(string id)
        {
            return _context.Users.Where(user => user.Id == id).FirstOrDefault();
        }

        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = getById(id);
            user.ProfileImageUrl = uri.AbsolutePath;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public interface IUserService{
        User getById(string id);
        IEnumerable<User> getAll();
        Task SetProfileImage(string id, Uri uri);
    }
}