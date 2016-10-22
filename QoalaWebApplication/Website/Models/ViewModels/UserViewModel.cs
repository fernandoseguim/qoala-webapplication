using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class UserViewModel
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Digite um e-mail válido!")]
        [Required(ErrorMessage = "Por favor, informe o email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha.")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Por favor, informe a permissão.")]
        [Display(Name = "Permissão")]
        [Range(1, 3, ErrorMessage = "Permissão deve ser entre 1 e 3")]
        public int Permission { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }
        [Display(Name = "Bairro")]
        public string District { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Display(Name = "Estado(UF)")]
        public string State { get; set; }
        [Display(Name = "CEP")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, HtmlEncode = false, DataFormatString = "99999-999", NullDisplayText = "")]
        public string ZipCode { get; set; }

        public string IdPlan { get; set; }

    }
}