namespace Website.Models
{
    public class Comment
    {
        public int IdPost { get; set; }
        public string Content { get; set; }
        public int IdUser { get; set; }
    }
}