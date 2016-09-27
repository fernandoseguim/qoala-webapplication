using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Principal;

namespace Website.Controllers
{
    public class ProfileController : BaseController
    {
        [AuthorizationRequest]
        public ActionResult Index()
        {
            // You need set currentUser to set viewbag dynamic.
            currentUser = (UserPrincipal) HttpContext.User;
            return View();
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit()
        {
            return View();
        }
    }
}