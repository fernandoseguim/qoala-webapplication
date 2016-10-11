using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        [AuthorizationRequest]
        public ActionResult Show()
        {
            var user = ((UserViewModel)Session["CurrentUser"]);
            var request = new WSRequest("users/" + user.Id_User);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();
            if (response.Code == 200)
            {
                var userModel = response.Body.ToObject<UserViewModel>();
                return View(userModel);
            }
            return View();
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit()
        {
            var user = ((UserViewModel)Session["CurrentUser"]);
            var request = new WSRequest("users/" + user.Id_User);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();
            if (response.Code == 200)
            {
                var userModel = response.Body.ToObject<UserViewModel>();
                return View(userModel);
            }
            return View();
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(UserViewModel model)
        {
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("email", model.Email),
                    new KeyValuePair<string, string>("permission", model.Permission.ToString()),
                    new KeyValuePair<string, string>("address", model.Address),
                    new KeyValuePair<string, string>("district", model.District ),
                    new KeyValuePair<string, string>("city", model.City ),
                    new KeyValuePair<string, string>("state", model.State ),
                    new KeyValuePair<string, string>("zipcode", model.ZipCode )
                };

            WSRequest request = new WSRequest("/users/" + model.Id_User);
            request.AddAuthorization(Session["token"].ToString());

            request.AddJsonParameter(parameters);

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Profile", new { message = "Não foi possivel editar o usuário" });

            return RedirectToAction("Show", "Profile", new { message = "Sucesso ao editar perfil" });
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idUser)
        {
            var user = (UserViewModel)Session["CurrentUser"];
            if (user.Id_User != idUser)
                return RedirectToAction("Show", "Profile");

            WSRequest request = new WSRequest("/users/" + idUser);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Delete();

            if (response.Code != 204)
                return RedirectToAction("Show", "Profile", new { message = "Não foi possivel deletar o usuário" });

            Session["token"] = null;
            Session["CurrentUser"] = null;
            var cookie = new HttpCookie("qoala_token");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}