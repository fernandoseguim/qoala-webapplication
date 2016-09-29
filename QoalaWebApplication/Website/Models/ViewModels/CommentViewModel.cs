using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class CommentViewModel
    {
        public int IdComment { get; set; }
        public int IdPost { get; set; }
        [Required]
        [Display(Name = "Conteúdo")]
        [StringLength(100, ErrorMessage = "O tamanho do nome deve respeitar o mínimo de {2} e máximo {1}", MinimumLength = 6)]
        public string Content { get; set; }
        public string ApprovedAt { get; set; }
        public int IdUser { get; set; }
    }
}