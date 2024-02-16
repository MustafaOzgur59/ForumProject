using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Entity;
using ForumProject.Models.Reply;

namespace ForumProject.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public string AuthorId{ get; set; }
        public string AuthorName{ get; set; }
        public string AuthorImageUrl{ get; set; }
        public int  AuthorRating{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public string Content{ get; set; }
        public IEnumerable<PostReplyModel> Replies{ get; set; }
    }
}