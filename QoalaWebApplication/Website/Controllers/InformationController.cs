using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class InformationController : Controller
    {
        [AuthorizationRequest]
        public ActionResult Index()
        {
            WSRequest request = new WSRequest("infos/");
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
            {
                return RedirectToAction("Index", "Information", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body.GetValue("infos");
            var model = new List<InformationViewModel>();
            foreach(var item in body)
            {
                model.Add(new InformationViewModel
                {
                    Key = item["key"].ToString(),
                    Value = item["value"].ToString()
                });
            }
            return View(model);
        }

        [AuthorizationRequest]
        public ActionResult Edit(string key)
        {
            WSRequest request = new WSRequest("infos/" + key);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
            {
                return RedirectToAction("Index", "Information", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;

            var info = new InformationViewModel
            {
                Key = body.GetValue("key").ToString(),
                Value = body.GetValue("value").ToString()
            };
            return View(info);
        }

        [AuthorizationRequest]
        [HttpPost]
        public ActionResult Delete(string key)
        {
            WSRequest request = new WSRequest("infos/" + key);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Delete();
            if (response.Code != 204)
            {
                return RedirectToAction("Index", "Information", new { message = "Não foi possível buscar esse post" });
            }
            return RedirectToAction("Index", "Information");
        }

        [AuthorizationRequest]
        [HttpPost]
        public ActionResult Update(InformationViewModel model)
        {
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("key", model.Key),
                    new KeyValuePair<string, string>("value", model.Value)
                };
            WSRequest request = new WSRequest("infos/" + model.Key);
            request.AddAuthorization(Session["token"].ToString());
            request.AddJsonParameter(parameters);
            var response = request.Put();

            if(response.Code != 204)
                return RedirectToAction("Index", "Information", new { message = "Não foi possível editar" });

            return RedirectToAction("Index", "Information");
        }

        [AuthorizationRequest]
        [HttpPost]
        public ActionResult Create(InformationViewModel model)
        {
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("key", model.Key),
                    new KeyValuePair<string, string>("value", model.Value)
                };
            WSRequest request = new WSRequest("infos/");
            request.AddAuthorization(Session["token"].ToString());
            request.AddJsonParameter(parameters);
            var response = request.Post();

            if (response.Code != 201)
                return RedirectToAction("Index", "Information", new { message = "Não foi possível criar" });

            return RedirectToAction("Index", "Information", new { message = "Informação criada com sucesso" });
        }

        [AuthorizationRequest]
        public ActionResult New()
        {
            return View();
        }

        [AuthorizationRequest]
        public ActionResult Show(string key)
        {
            WSRequest request = new WSRequest("infos/" + key);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();
            if (response.Code != 200)
            {
                return RedirectToAction("Index", "Information", new { message = "Não foi possível buscar esse post" });
            }
            var body = response.Body;

            var info = new InformationViewModel
            {
                Key = body.GetValue("key").ToString(),
                Value = body.GetValue("value").ToString()
            };
            return View(info);
        }

        
        public ActionResult Render(string key)
        {
            WSRequest request = new WSRequest("infos/" + key);
            var response = request.Get();
            var body = response.Body;

            var info = new InformationViewModel
            {
                Key = body.GetValue("key").ToString(),
                Value = body.GetValue("value").ToString()
            };
            return View(info);
        }
    }
}