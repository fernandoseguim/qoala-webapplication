using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class InformationViewModel
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}