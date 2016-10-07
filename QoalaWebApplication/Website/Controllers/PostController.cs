using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        [AuthorizationRequest]
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
                        IdComment = (int)comment["id_comment"],
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
        public ActionResult Index(int page = 1)
        {
            var user = (UserViewModel)Session["CurrentUser"];
            WSRequest request = null;
            if (user.Permission == 3)
            {
                request = new WSRequest("/posts?page=" + page);
            } else
            {
                request = new WSRequest("users/" + user.IdUser + "/posts?page=" + page);
            }

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Home");
            
            var body = response.Body;

            PostListViewModel model = new PostListViewModel();
            var pagination = body.GetValue("pagination");
            model.Pagination = new PaginationViewModel
            {
                NextPage = (bool)pagination["next_page"],
                PreviousPage = (bool)pagination["previous_page"],
                CurrentPage = (int)pagination["current_page"],
                TotalNumberPages = (int)pagination["total_number_pages"],
                ControllerName = "Post"
            };
            model.Posts = new List<PostViewModel>();
            foreach (var post in body["posts"])
            {
                model.Posts.Add(
                    new PostViewModel
                    {
                        ContentSummary = post["content"].ToString(),
                        PublishedAt = post["published_at"].ToString(),
                        IdPost = (int)post["id_post"],
                        IdUser = (int)post["id_user"],
                        Title = post["title"].ToString(),
                        UserName = post["user_name"].ToString()
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
            
            return RedirectToAction("Show", "Post", new { idPost = response.Body.GetValue("id_post") });
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit(int idPost)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            var response = request.Get();
            if (response.Code != 200)
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
                Title = body["title"].ToString()
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(PostViewModel post)
        {
            WSRequest request = new WSRequest("/posts/" + post.IdPost);
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("title", post.Title),
                    new KeyValuePair<string, string>("content", post.Content)
                };

            request.AddJsonParameter(parameters);

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Post", post);

            return RedirectToAction("Show", "Post", new { idPost = post.IdPost, message = "O post " + post.IdPost + " foi atualizado"});
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idPost, string returnUrl)
        {
            WSRequest request = new WSRequest("posts/" + idPost);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Delete();
            if (response.Code != 204)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            if (returnUrl != null)
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Post", new { message = "Post " + idPost + " foi deletado."});
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Publish(int idPost, string returnUrl)
        {
            WSRequest request = new WSRequest("posts/" + idPost + "/publish");
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Put();
            if (response.Code != 204)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });

            if(returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("Show", "Post", new { idPost = idPost, message = "O post " + idPost + " foi publicado" });
        }
    }
}