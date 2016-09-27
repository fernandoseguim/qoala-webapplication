using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Principal;
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
            
            UserPrincipal currentUser = new UserPrincipal(body.GetValue("name").ToString());
            currentUser.Id =  (int)body.GetValue("id_user");
            currentUser.Name = body.GetValue("name").ToString();
            currentUser.Email = body.GetValue("email").ToString();
            currentUser.Permission = (int)body.GetValue("permission");

            HttpContext.Current.User = currentUser;
        }

        private RouteValueDictionary routeValuesRedirect()
        {
            return new RouteValueDictionary(new
            {
                action = "Login",
                controller = "Account",
                Message = "Você precisa estar logado para fazer essa ação",
            });
        }
    }
}