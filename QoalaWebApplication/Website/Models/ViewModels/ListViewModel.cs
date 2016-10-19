using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class ListViewModel<T>
    {
        public List<T> ListModel { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public ListViewModel() {
            ListModel = new List<T>();
            Pagination = new PaginationViewModel();
        }
    }
}