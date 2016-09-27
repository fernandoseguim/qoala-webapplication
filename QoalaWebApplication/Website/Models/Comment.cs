using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Comment
    {
        public int IdPost { get; set; }
        [Required]
        [Display(Name = "Conteúdo")]
        [StringLength(100, ErrorMessage = "O tamanho do nome deve respeitar o mínimo de {2} e máximo {1}", MinimumLength = 6)]
        public string Content { get; set; }
        public int IdUser { get; set; }
    }
}