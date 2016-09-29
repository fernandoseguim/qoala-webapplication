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

        [HttpGet]
        [Route("blog/posts/{idPost}")]
        public ActionResult ShowPost(int idPost)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            var response = request.Get();
            if (response.Code != 200)
            {
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;

            List<CommentViewModel> comments = new List<CommentViewModel>();
            foreach (var comment in body["comments"])
            {
                comments.Add(
                    new CommentViewModel
                    {
                        IdPost = (int)comment["id_post"],
                        Content = comment["content"].ToString(),
                        IdUser = (int)comment["id_user"]
                    }
                );
            }

            var model = new PostViewModel
            {
                Content = body["content"].ToString(),
                PublishedAt = body["published_at"].ToString(),
                IdPost = (int)body["id_post"],
                IdUser = (int)body["id_user"],
                Title = body["title"].ToString(),
                Comments = comments
            };

            return View(model);
        }
    }
}