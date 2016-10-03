using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}