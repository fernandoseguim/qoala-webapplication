using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class BlogViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public int TotalNumberPages { get; set; }
        public int CurrentPage { get; set; }
    }
}