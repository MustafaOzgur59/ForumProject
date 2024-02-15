using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Entity
{
    public class User : IdentityUser
    {
        public int Rating { get; set; }
        public string ProfileImageUrl{ get; set; }
        public DateTime CreateDate{ get; set; }
        public bool IsActive{ get; set; }
    }
}