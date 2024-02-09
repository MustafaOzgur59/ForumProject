using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Entity
{
    public class PostReply
    {
        [Key]
        public int Id { get; set; }
        public String Content { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}