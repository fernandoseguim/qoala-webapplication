using System.Collections.Generic;
using System.Web.Mvc;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [Route("blog")]
        public ActionResult Index(int page = 1)
        {
            WSRequest request = new WSRequest("posts?page=" + page);
            var response = request.Get();
            var model = new BlogViewModel { };
            if (response.Code == 200)
            {
                var body = response.Body;
                List<PostViewModel> posts = new List<PostViewModel>();
                foreach(var post in body.GetValue("posts"))
                {
                    posts.Add(new PostViewModel
                    {
                        IdPost = (int) post["id_post"],
                        Title = post["title"].ToString(),
                        ContentSummary = post["content"].ToString(),
                        PublishedAt = post["published_at"].ToString(),
                        IdUser = (int) post["id_user"],
                    });
                }

                model.Posts = posts;
                model.TotalNumberPages = (int) body.GetValue("total_number_pages");
                model.PreviousPage = (bool) body.GetValue("previous_page");
                model.NextPage = (bool) body.GetValue("next_page");
                model.CurrentPage = (int)body.GetValue("current_page");
            }
            return View(model);
        }
    }
}