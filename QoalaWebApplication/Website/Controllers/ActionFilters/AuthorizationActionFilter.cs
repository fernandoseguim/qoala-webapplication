using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Principal;

namespace Website.Controllers.ActionFilters
{
    public class AuthorizationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = HttpContext.Current.Session["token"];
            if(token == null)
            {
                filterContext.Result = new RedirectToRouteResult(routeValuesRedirect());
                return;
            }
            API.WSRequest request = new API.WSRequest("accounts/validatetoken");

            request.AddAuthorization(token.ToString());

            var response = request.Post();
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