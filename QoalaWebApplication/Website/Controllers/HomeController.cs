using System.Web.Hosting;
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

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(HostingEnvironment.MapPath(@"~/App_Data/plural.txt"));
            string fileName = "plural.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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