using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            API.WSRequest request = new API.WSRequest("accounts/validadetoken");

            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("token", token.ToString())
            };

            request.AddAuthorization(token.ToString());
            request.AddJsonParameter(parameters);

            var response = request.Post();
            if (response.Code != 202)
                filterContext.Result = new RedirectToRouteResult(routeValuesRedirect());
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