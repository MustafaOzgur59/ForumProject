using ForumProject.Models.Forum;

namespace ForumProject.Models.Post
{
    public class PostListModel
    {
        public int Id{ get; set; }
        public string Title{ get; set; }
        public string AuthorName { get; set; }
        public string AuthorRating { get; set;}
        public string AuthorId{ get; set; }
        public DateTime DatePosted { get; set; }

        public ForumViewModel Forum { get; set; }
        public int ReplyCount{ get; set; }

    }   
}