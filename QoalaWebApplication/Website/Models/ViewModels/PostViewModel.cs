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
        [System.Web.Mvc.AllowHtml]
        public string Content { get; set; }

        public string ContentSummary
        {
            get
            {
                return _contentSummary;
            }
            set
            {
                string content = StripHtml(value);
                int limit = content.Length < 100 ? content.Length : 100;
                _contentSummary  = content.Substring(0, limit - 1);
            }
        }
        [Display(Name = "Publicado em")]
        public string PublishedAt { get; set; }
        [Required]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        private string StripHtml(string html)
        {
            return Regex.Replace(html, "<(.|\\n)*?>", string.Empty); 
        }
    }
}