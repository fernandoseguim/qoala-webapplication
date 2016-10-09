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
                model.Posts = new List<PostViewModel>();
                foreach(var post in body.GetValue("posts"))
                {
                    model.Posts.Add(new PostViewModel
                    {
                        IdPost = (int) post["id_post"],
                        Title = post["title"].ToString(),
                        Content = post["content"].ToString(),
                        PublishedAt = post["published_at"].ToString(),
                        IdUser = (int) post["id_user"],
                        UserName = post["user_name"].ToString()
                    });
                }
                var pagination = body.GetValue("pagination");
                model.Pagination = new PaginationViewModel
                {
                    NextPage = (bool)pagination["next_page"],
                    PreviousPage = (bool)pagination["previous_page"],
                    CurrentPage = (int)pagination["current_page"],
                    TotalNumberPages = (int)pagination["total_number_pages"],
                };
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
                        UserName = comment["user_name"].ToString()
                    }
                );
            }

            var model = new PostViewModel
            {
                Content = body["content"].ToString(),
                PublishedAt = body["published_at"].ToString(),
                IdPost = (int)body["id_post"],
                IdUser = (int)body["id_user"],
                UserName = body["user_name"].ToString(),
                Title = body["title"].ToString(),
                Comments = comments
            };

            return View(model);
        }
    }
}