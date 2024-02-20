using System.ComponentModel.DataAnnotations;

namespace ForumProject.Entity
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual IEnumerable<PostReply> PostReplies { get; set; }
    }
}