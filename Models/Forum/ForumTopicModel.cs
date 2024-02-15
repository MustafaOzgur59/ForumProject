using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.Post;

namespace ForumProject.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumViewModel Forum{ get; set; }
        public IEnumerable<PostListModel> Posts{ get; set; }
    }
}