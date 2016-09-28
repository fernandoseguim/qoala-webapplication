using System.Web.Mvc;
using Website.Controllers.ActionFilters;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        [AuthorizationRequest]
        public ActionResult Index()
        {
            // You need set currentUser to set viewbag dynamic.
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