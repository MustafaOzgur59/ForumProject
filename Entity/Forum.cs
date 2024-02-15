using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Entity
{
    public class Forum
    {
        public int Id{ get; set; }
        public String Title{ get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set;}
        public DateTime CreateTime { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}