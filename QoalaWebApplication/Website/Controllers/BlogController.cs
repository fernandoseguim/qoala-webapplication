using System.Collections.Generic;
using System.Web.Mvc;
using Website.Models;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [Route("blog/{page}")]
        public ActionResult Index(int page = 1)
        {
            WSRequest request = new WSRequest("posts?pageNumber=" + page);
            var response = request.Get();
            var model = new BlogViewModel { };
            if (response.Code == 200)
            {
                var body = response.Body;
                List<Post> posts = new List<Post>();
                foreach(var post in body.GetValue("posts"))
                {
                    posts.Add(new Post
                    {
                        IdPost = (int) post["id_post"],
                        Title = post["title"].ToString(),
                        Content = post["content"].ToString(),
                        PublishedAt = post["published_at"].ToString(),
                        IdUser = (int) post["id_user"],
                    });
                }

                model.Posts = posts;
                model.TotalNumberPages = (int)body.GetValue("total_number_page");
                model.HasMorePages = (bool) body.GetValue("has_more_pages");
            }
            return View(model);
        }
    }
}