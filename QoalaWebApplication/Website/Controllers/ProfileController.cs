using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Principal;

namespace Website.Controllers
{
    public class ProfileController : BaseController
    {
        [AuthorizationActionFilter]
        public ActionResult Index()
        {
            // You need set currentUser to set viewbag dynamic.
            currentUser = (UserPrincipal) HttpContext.User;
            return View();
        }

        [HttpGet]
        [AuthorizationActionFilter]
        public ActionResult Edit()
        {
            return View();
        }
    }
}