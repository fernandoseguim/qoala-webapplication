using System.Web.Mvc;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        public ActionResult Get(int idPost)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            var response = request.Get();
            if(response.Code != 200)
            {
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;
            var model = new PostViewModel
            {
                Content = body["content"].ToString(),
                PublishedAt = body["published_at"].ToString(),
                IdPost = (int)body["id_post"],
                IdUser = (int)body["id_user"],
                Title = body["title"].ToString(),
            };

            return View(model);
        }
    }
}