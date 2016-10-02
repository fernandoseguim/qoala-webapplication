using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class DeviceViewModel
    {
        public int IdDevice { get; set; }
        [Required]
        [Display(Name = "Alarme")]
        public bool Alarm { get; set; }

        [Required(ErrorMessage = "Como deseja identificar o device?")]
        [Display(Name = "Apelido")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Escolha uma cor para facilitar a identificação do device")]
        [Display(Name = "Cor")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Qual a frequencia que deseja rastrear seu device")]
        [Display(Name = "Frequencia de atualização")]
        public int FrequencyUpdate { get; set; }
        public int IdUser { get; set; }
    }
}