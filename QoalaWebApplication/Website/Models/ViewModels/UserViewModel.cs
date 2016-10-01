using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class UserViewModel 
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha.")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Por favor, informe a permissão.")]
        [Display(Name = "Permissão")]
        [Range(1, 3, ErrorMessage = "Square Feet must be a positive number")]
        public int Permission { get; set; }
    }
}