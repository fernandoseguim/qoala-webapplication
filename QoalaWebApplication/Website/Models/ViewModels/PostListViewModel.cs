using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class PostListViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}