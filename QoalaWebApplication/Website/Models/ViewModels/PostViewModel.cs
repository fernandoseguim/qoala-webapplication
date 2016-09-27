using Newtonsoft.Json.Linq;

namespace Website.Models.ViewModels
{
    public class PostViewModel
    {
        public int idPost { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string publishedAt { get; set; }
        public int idUser { get; set; }
        public bool hasMorePages { get; set; }
        public int pageTotal { get; set; }

        public PostViewModel(JToken json)
        {
            idPost = (int) json["id_post"];
            title = json["title"].ToString();
            content = json["content"].ToString();
            publishedAt = json["published_at"].ToString();
            idUser = (int) json["id_user"];
        }
    }
}