using System.Collections.Generic;
using System.Web.Mvc;
using Website.Models;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        [Route("posts/{idPost}")]
        public ActionResult GetPost(int idPost)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            var response = request.Get();
            if(response.Code != 200)
            {
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;

            List<Comment> comments = new List<Comment>();
            foreach (var comment in body["comments"])
            {
                comments.Add(
                    new Comment
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