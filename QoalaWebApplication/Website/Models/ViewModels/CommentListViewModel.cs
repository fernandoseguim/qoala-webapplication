using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class CommentListViewModel
    {
        public List<CommentViewModel> Comments { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}