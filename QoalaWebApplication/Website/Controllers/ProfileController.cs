using System.Web.Mvc;
using Website.Controllers.ActionFilters;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        [AuthorizationActionFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}