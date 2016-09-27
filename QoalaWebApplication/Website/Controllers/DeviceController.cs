using System.Web.Mvc;
using Website.Controllers.ActionFilters;

namespace Website.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        [AuthorizationRequest]
        public ActionResult Index()
        {
            return View();
        }
    }
}