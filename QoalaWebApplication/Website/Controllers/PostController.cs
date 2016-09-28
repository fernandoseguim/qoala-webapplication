using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        [Route("posts/{idPost}")]
        public ActionResult Show(int idPost)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            var response = request.Get();
            if(response.Code != 200)
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

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Index()
        {
            var user = (UserViewModel)Session["CurrentUser"];
            WSRequest request = new WSRequest("users/"+ user.IdUser + "/posts");

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Home");
            
            var body = response.Body;

            List<PostViewModel> model = new List<PostViewModel>();
            foreach (var post in body["posts"])
            {
                model.Add(
                    new PostViewModel
                    {
                        ContentSummary = post["content"].ToString(),
                        PublishedAt = post["published_at"].ToString(),
                        IdPost = (int)post["id_post"],
                        IdUser = (int)post["id_user"],
                        Title = post["title"].ToString()
                    }
                );
            };

            return View(model);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult New()
        {
            var user = (UserViewModel) Session["CurrentUser"];
            return View(new PostViewModel { IdUser = user.IdUser });
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Create(PostViewModel post)
        {
            WSRequest request = new WSRequest("/posts");
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("title", post.Title),
                    new KeyValuePair<string, string>("content", post.Content),
                    new KeyValuePair<string, string>("id_user", post.IdUser.ToString())
                };

            request.AddJsonParameter(parameters);

            var response = request.Post();

            if (response.Code != 201)
                return RedirectToAction("New", "Post", new { error = "Não foi possivel cadastrar" });
            
            return RedirectToAction("Show", "Post", new { IdPost = response.Body["id_post"] });
        }
    }
}