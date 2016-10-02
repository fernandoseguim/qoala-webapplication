using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class DeviceListViewModel
    {
        public List<DeviceViewModel> Devices { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}