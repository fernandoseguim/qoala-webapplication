using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Website.Models.ViewModels
{
    public class PostViewModel
    {
        private string _contentSummary;
        public int IdPost { get; set; }

        [Required(ErrorMessage = "Por favor, informe o título.")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Por favor, informe o conteúdo.")]
        [Display(Name = "Conteúdo")]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string ContentSummary
        {
            get
            {
                return _contentSummary;
            }
            set
            {
                int limit = value.Length < 100 ? value.Length : 100;
                _contentSummary = StripHtml(value).Substring(0, limit - 1);
            }
        }
        [Display(Name = "Publicado em")]
        public string PublishedAt { get; set; }
        [Required]
        public int IdUser { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        private string StripHtml(string html)
        {
            return Regex.Replace(html, "<(.|\\n)*?>", string.Empty); 
        }
    }
}