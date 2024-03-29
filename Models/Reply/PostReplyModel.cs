using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.Reply
{
    public class PostReplyModel
    {
        public int Id{ get; set; }
        public string AuthorId{ get; set; }
        public string AuthorName{ get; set; }
        public int AuthorRating{ get; set; }
        public string AuthorImageUrl{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public string Content{ get; set; }
        public int PostId{ get; set; }
    }
}