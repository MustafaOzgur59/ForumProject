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
        public String? Title{ get; set; }
        public String? AuthorId{ get; set; }
        public String? AuthorName{ get; set; }
        public String? AuthorImageUrl{ get; set; }
        public int  AuthorRating{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public String? Content{ get; set; }
        public IEnumerable<PostReplyModel> Replies{ get; set; }
        public String? ForumName { get; set; }
        public int ForumId{ get; set; }
    }
}