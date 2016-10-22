using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Models.API;

namespace Website.Controllers.ActionFilters
{
    public class AuthorizationRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = HttpContext.Current.Session["token"];
            if(token == null)
            {
                filterContext.Result = new RedirectToRouteResult(routeValuesRedirect());
                return;
            }

            WSRequest request = new WSRequest("accounts/me");

            request.AddAuthorization(token.ToString());

            var response = request.Get();
            if (response.Code != 200)
            {
                filterContext.Result = new RedirectToRouteResult(routeValuesRedirect());
                return;
            }
            var body = response.Body;

            Models.ViewModels.UserViewModel user = new Models.ViewModels.UserViewModel
            {
                IdUser = (int)body.GetValue("id_user"),
                Email = body.GetValue("email").ToString(),
                Name = body.GetValue("name").ToString(),
                IdPlan = body.GetValue("id_plan").ToString(),
                Permission = (int)body.GetValue("permission")
            };

            HttpContext.Current.Session["CurrentUser"] = user;
        }

        private RouteValueDictionary routeValuesRedirect()
        {
            return new RouteValueDictionary(new
            {
                action = "Index",
                controller = "Home",
                Message = "Você precisa estar logado para fazer essa ação",
            });
        }
    }
}