using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.Forum
{
    public class ForumViewModel
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }

        public string ImageUrl{ get; set; }
    }
}