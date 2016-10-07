namespace Website.Models.ViewModels
{
    public class PaginationViewModel
    {
        public int TotalNumberPages { get; set; }
        public bool NextPage { get; set; }
        public int CurrentPage { get; set; }
        public bool PreviousPage { get; set; }
        public string ControllerName { get; set; }
    }
}