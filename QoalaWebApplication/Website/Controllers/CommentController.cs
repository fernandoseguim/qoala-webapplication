using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Principal;
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
            var user = (UserPrincipal) HttpContext.User;
            WSRequest request = new WSRequest("posts/" + comment.IdPost.ToString() + "/comments");
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id_user", user.Id.ToString()),
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
    }
}