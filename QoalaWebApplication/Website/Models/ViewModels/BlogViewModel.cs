using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class BlogViewModel
    {
        public List<Post> Posts { get; set; }
        public bool HasMorePages { get; set; }
        public int TotalNumberPages { get; set; }
    }
}