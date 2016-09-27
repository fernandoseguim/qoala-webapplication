using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class PostViewModel
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishedAt { get; set; }
        public int IdUser { get; set; }
        public List<Comment> Comments { get; set; }
    }
}