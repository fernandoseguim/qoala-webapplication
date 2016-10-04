using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class CommentViewModel
    {
        public string ReturnUrl { get; set; }
        public int IdComment { get; set; }
        public int IdPost { get; set; }
        [Required]
        [Display(Name = "Conteúdo")]
        [StringLength(100, ErrorMessage = "O tamanho do nome deve respeitar o mínimo de {2} e máximo {1}", MinimumLength = 6)]
        public string Content { get; set; }
        [Display(Name = "Aprovado em")]
        public string ApprovedAt { get; set; }
        [Display(Name = "Criado em")]
        public string CreatedAt { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
    }
}