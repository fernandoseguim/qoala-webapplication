using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Post
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishedAt { get; set; }
        public int IdUser { get; set; }
    }
}