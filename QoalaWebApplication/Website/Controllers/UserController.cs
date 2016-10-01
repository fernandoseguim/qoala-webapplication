using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        [AuthorizationRequest]
        public ActionResult Index()
        {
            WSRequest request = new WSRequest("users");
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Show", "Profile");

            var body = response.Body;

            List<UserViewModel> model = new List<UserViewModel>();
            foreach (var user in body["users"])
            {
                model.Add(
                    new UserViewModel
                    {
                        IdUser = (int) user["id_user"],
                        Name = user["name"].ToString(),
                        Email = user["email"].ToString(),
                        Permission = (int) user["permission"]
                    }
                );
            };

            return View(model);
        }

        [AuthorizationRequest]
        public ActionResult Show(int idUser)
        {
            WSRequest request = new WSRequest("users/" + idUser);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "User");

            var body = response.Body;

            var model = new UserViewModel
            {
                IdUser = (int)body["id_user"],
                Name = body["name"].ToString(),
                Email = body["email"].ToString(),
                Permission = (int)body["permission"]
            };

            return View(model);
        }
        
        [AuthorizationRequest]
        public ActionResult Edit(int idUser)
        {
            WSRequest request = new WSRequest("users/" + idUser);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "User");

            var body = response.Body;

            var model = new UserViewModel
            {
                IdUser = (int)body["id_user"],
                Name = body["name"].ToString(),
                Email = body["email"].ToString(),
                Permission = (int)body["permission"]
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(UserViewModel model)
        {
            WSRequest request = new WSRequest("/users/" + model.IdUser);
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("email", model.Email),
                    new KeyValuePair<string, string>("permission", model.Permission.ToString()),
                };

            request.AddJsonParameter(parameters);

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "User", model);

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idUser)
        {
            WSRequest request = new WSRequest("/users/" + idUser);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Delete();

            if (response.Code != 204)
                return RedirectToAction("Index", "User", new { message = "Não foi possivel deletar o usuário" });

            return RedirectToAction("Index", "User");
        }
    }
}