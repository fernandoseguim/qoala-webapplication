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
                }
            }
        }
    }
}