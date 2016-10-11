using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class CommentController : Controller
    {
        [AuthorizationRequest]
        [HttpPost]
        [Route("comments")]
        public JsonResult New(CommentViewModel comment)
        {
            var user = (UserViewModel) Session["currentUser"];
            WSRequest request = new WSRequest("posts/" + comment.IdPost.ToString() + "/comments");
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id_user", user.IdUser.ToString()),
                    new KeyValuePair<string, string>("id_post", comment.IdPost.ToString()),
                    new KeyValuePair<string, string>("content", comment.Content),
                };

            request.AddJsonParameter(parameters);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Post();
            var data = new { success = false };
            if (response.Code != 201)
                return new JsonResult() { Data = data };
            
            data = new { success = true };
            return new JsonResult() { Data = data };
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Index(int page = 1)
        {
            var user = (UserViewModel) Session["CurrentUser"];
            WSRequest request = new WSRequest("users/" + user.IdUser + "/posts/comments?page=" + page);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            
            var body = response.Body;
            CommentListViewModel model = new CommentListViewModel();
            var pagination = body.GetValue("pagination");
            model.Pagination = new PaginationViewModel
            {
                NextPage = (bool)pagination["next_page"],
                PreviousPage = (bool)pagination["previous_page"],
                CurrentPage = (int)pagination["current_page"],
                TotalNumberPages = (int)pagination["total_number_pages"],
            };
            model.Comments = new List<CommentViewModel>();
            foreach (var comment in body.GetValue("comments"))
            {
                model.Comments.Add(
                    new CommentViewModel
                    {
                        IdComment = (int)comment["id_comment"],
                        IdPost = (int)comment["id_post"],
                        Content = comment["content"].ToString(),
                        IdUser = (int)comment["id_user"],
                        ApprovedAt = comment["approved_at"].ToString(),
                        CreatedAt = comment["created_at"].ToString()
                    }
                );
            }

            return View(model);
        }
        
        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idComment, int idPost, string returnUrl)
        {
            WSRequest request = new WSRequest("posts/" + idPost + "/comments/" + idComment);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Delete();
            if (response.Code != 204)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível deletar o comentário" });

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Comment", new { message = "O comentário " + idComment + " foi deletado." });
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit(int idComment, int idPost)
        {
            WSRequest request = new WSRequest("/posts/" + idPost + "/comments/" + idComment);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
                return RedirectToAction("Index", "Comment", new { message = "Não foi possível buscar o comentário" });
            var body = response.Body;

            CommentViewModel model = new CommentViewModel
            {
                IdComment = idComment,
                IdPost = idPost,
                Content = body.GetValue("content").ToString(),
                ReturnUrl = Request.Params.Get("returnUrl").ToString()
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(string returnUrl, CommentViewModel comment)
        {
            WSRequest request = new WSRequest("/posts/" + comment.IdPost + "/comments/" + comment.IdComment);
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("content", comment.Content)
                };

            request.AddJsonParameter(parameters);

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Comment", comment);

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("Show", "Comment", new { idPost = comment.IdPost, idComment = comment.IdComment, message = "O comentário " + comment.IdComment + " foi atualizado" });
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Show(int idComment, int idPost)
        {
            WSRequest request = new WSRequest("/posts/" + idPost + "/comments/" + idComment);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Comment", new { message = "Não foi possível exibir o comentário" });

            var body = response.Body;

            CommentViewModel model = new CommentViewModel
            {
                IdComment = idComment,
                IdPost = idPost,
                Content = body.GetValue("content").ToString(),
                CreatedAt = body.GetValue("created_at").ToString(),
                ApprovedAt = body.GetValue("approved_at").ToString()
            };
            return View(model);
        }
    }
}