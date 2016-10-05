using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NewLayout()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewDevice()
        {
            return View();
        }

        [Route("sobre")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("contato")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("time")]
        public ActionResult Team()
        {
            ViewBag.Message = "Informações sobre a equipe.";

            return View();
        }

    }
}