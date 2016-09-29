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
        public ActionResult Index()
        {
            var user = (UserViewModel) Session["CurrentUser"];
            WSRequest request = new WSRequest("users/" + user.IdUser + "/posts/comments");
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            
            var body = response.Body;
            List<CommentViewModel> model = new List<CommentViewModel>();
            foreach (var comment in body.GetValue("comments"))
            {
                model.Add(
                    new CommentViewModel
                    {
                        IdPost = (int)comment["id_post"],
                        Content = comment["content"].ToString(),
                        IdUser = (int)comment["id_user"]
                    }
                );
            }

            return View(model);
        }
    }
}