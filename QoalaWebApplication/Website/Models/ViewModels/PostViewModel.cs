using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Website.Models.ViewModels
{
    public class PostViewModel
    {
        private string _contentSummary;
        public int IdPost { get; set; }
        public string Title { get; set; }
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
        public string PublishedAt { get; set; }
        public int IdUser { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        private string StripHtml(string html)
        {
            return Regex.Replace(html, "<(.|\\n)*?>", string.Empty); 
        }
    }
}