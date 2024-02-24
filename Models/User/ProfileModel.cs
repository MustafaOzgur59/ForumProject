using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.Post;

namespace ForumProject.Models.User
{
    public class ProfileModel
    {
        public string UserId{ get; set; }
        public string Email{ get; set; }
        public string UserName{ get; set; }
        public string UserRating { get; set; }
        public string ProfileImageUrl{ get; set; }
        public DateTime CreateDate{ get; set; }
        public IFormFile ImageUpload{ get; set; }
        public bool IsAdmin{ get; set; }
        public IEnumerable<PostListModel> Posts{ get; set; }
    }
}