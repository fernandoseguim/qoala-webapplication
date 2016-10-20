using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;
using Website.Models.ViewModels.Sponsor;

namespace Website.Controllers
{
    public class PlanController : Controller
    {
        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Show(int idPlan)
        {
            WSRequest request = new WSRequest("plans/" + idPlan);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
            {
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;
            //var js = new Newtonsoft.Json.JsonSerializer();
            //var mod=js.Deserialize(new System.IO.StringReader(body.ToString()), typeof(PlanViewModel));

            var model = new PlanViewModel
            {
                IdPlan = (int)body["id_plan"],
                Name = body["name"].ToString(),
                Left = (int)body["left"],
                Price_cents = (int)body["price_cents"],
                Rewards = body["rewards"].ToString(),
            };

            return View(model);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Index(int page = 1)
        {
            var user = (UserViewModel)Session["CurrentUser"];
            WSRequest request = null;
            request = new WSRequest("/plans?page=" + page);

            var response = request.Get();
            ListViewModel<PlanViewModel> model = new ListViewModel<PlanViewModel>("Plan");
            //model.ListModel = new List<PlanViewModel>();
            //model.Pagination = new PaginationViewModel();

            if (response.Code == 200)
            {
                var body = response.Body;

                var pagination = body.GetValue("pagination");
                if (pagination != null)
                {
                    model.Pagination = new PaginationViewModel
                    {
                        NextPage = (bool)pagination["next_page"],
                        PreviousPage = (bool)pagination["previous_page"],
                        CurrentPage = (int)pagination["current_page"],
                        TotalNumberPages = (int)pagination["total_number_pages"],
                        ControllerName = "Plan"
                    };
                }
                foreach (var plan in body["plans"])
                {
                    model.ListModel.Add(
                        new PlanViewModel
                        {
                            IdPlan = (int)plan["id_plan"],
                            Name = plan["name"].ToString(),
                            Left = (int)plan["left"],
                            Price_cents = (int)plan["price_cents"],
                            Rewards = plan["rewards"].ToString(),
                        }
                    );
                };
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult New()
        {
            return View(new PlanViewModel() { Price_cents = 50000 });
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Create(PlanViewModel plan)
        {
            if (ModelState.IsValid)
            {
                WSRequest request = new WSRequest("/plans");
                request.AddAuthorization(Session["token"].ToString());
                IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", plan.Name),
                    //new KeyValuePair<string, string>("id_plan", plan.IdPlan.ToString()),
                    new KeyValuePair<string, string>("left", plan.Left.ToString()),
                    new KeyValuePair<string, string>("price_cents", plan.Price_cents.ToString()),
                    new KeyValuePair<string, string>("rewards", plan.Rewards),
                };

                request.AddJsonParameter(parameters);

                var response = request.Post();

                if (response.Code != 201)
                {
                    ModelState.AddModelError("", response.Code + ":" + response.Body.GetValue("Message").ToString());
                }
                else
                {
                    //return RedirectToAction("Show", "Plan", new { idPlan = response.Body.GetValue("id_plan") });
                    return RedirectToAction("Show", "Plan", new { idPlan = plan.IdPlan });
                }
            }
            return View("New", plan); //RedirectToAction("New", "Plan", new { error = "Não foi possivel cadastrar" });
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit(int idPlan)
        {
            WSRequest request = new WSRequest("plans/" + idPlan);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
            {
                ModelState.AddModelError("", "Não foi possível buscar esse plano");

                return RedirectToAction("Index", "Plan");
            }
            var body = response.Body;

            var model = new PlanViewModel
            {
                IdPlan = (int)body["id_plan"],
                Name = body["name"].ToString(),
                Left = (int)body["left"],
                Price_cents = (int)body["price_cents"],
                Rewards = body["rewards"].ToString(),
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(PlanViewModel plan)
        {
            WSRequest request = new WSRequest("/plans/" + plan.IdPlan);
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", plan.Name),
                    new KeyValuePair<string, string>("left", plan.Left.ToString()),
                    new KeyValuePair<string, string>("price_cents", plan.Price_cents.ToString()),
                    new KeyValuePair<string, string>("rewards", plan.Rewards),
                };

            request.AddJsonParameter(parameters);

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Plan", plan);

            return RedirectToAction("Show", "Plan", new
            {
                idPlan = plan.IdPlan,
                message = string.Format("O plano {0} - {1} foi atualizado", plan.IdPlan, plan.Name)
            });
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idPlan, string returnUrl)
        {
            WSRequest request = new WSRequest("plans/" + idPlan);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Delete();
            if (response.Code != 204)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar esse plano" });
            if (returnUrl != null)
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Plan", new { message = "Plano " + idPlan + " foi deletado." });
        }

    }
}