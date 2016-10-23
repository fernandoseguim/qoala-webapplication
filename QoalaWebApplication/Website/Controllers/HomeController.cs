using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;
using Website.Models.API;
using Website.Models.ViewModels;
using Website.Models.ViewModels.Sponsor;

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
            var user = (UserViewModel)Session["CurrentUser"];
            if(user != null && user.Permission == 4)
            {
                WSRequest request = new WSRequest("plans/");
                var response = request.Get();
                if (response.Code != 200)
                {
                    ModelState.AddModelError("", "Não foi possível buscar esse plano");
                    return RedirectToAction("Index", "Home", "ERRO");
                }
                var body = response.Body;
                List<PlanViewModel> list = new List<PlanViewModel>();

                foreach (var item in body.GetValue("plans"))
                {
                    list.Add(
                        new PlanViewModel
                        {
                            IdPlan = (int)item["id_plan"],
                            Left = (int)item["left"],
                            Name = item["name"].ToString(),
                            Price_cents = (int)item["price_cents"],
                            Rewards = item["rewards"].ToString(),
                        }
                    );
                }
                ViewBag.plans = list;
            }
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