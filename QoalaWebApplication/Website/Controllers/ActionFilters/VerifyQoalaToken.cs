using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Models.API;

namespace Website.Controllers.ActionFilters
{
    public class VerifyQoalaToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = HttpContext.Current.Session["token"];
            if(token == null)
            {
                if(HttpContext.Current.Request.Cookies["qoala_token"] != null)
                {
                    token = HttpContext.Current.Request.Cookies["qoala_token"].Value;
                    HttpContext.Current.Session["token"] = token;

                    WSRequest request = new WSRequest("accounts/me");

                    request.AddAuthorization(token.ToString());

                    var response = request.Get();
                    if (response.Code != 200)
                    {
                        return;
                    }
                    var body = response.Body;

                    Models.ViewModels.UserViewModel user = new Models.ViewModels.UserViewModel
                    {
                        IdUser = (int)body.GetValue("id_user"),
                        Email = body.GetValue("email").ToString(),
                        Name = body.GetValue("name").ToString(),
                        Permission = (int)body.GetValue("permission")
                    };

                    HttpContext.Current.Session["CurrentUser"] = user;
                }
            }
        }
    }
}