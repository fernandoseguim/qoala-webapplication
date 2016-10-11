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
        public ActionResult Index(int page = 1)
        {
            WSRequest request = new WSRequest("users/?page=" + page);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Show", "Profile");

            var body = response.Body;

            UserListViewModel model = new UserListViewModel();
            var pagination = body.GetValue("pagination");
            model.Pagination = new PaginationViewModel
            {
                NextPage = (bool)pagination["next_page"],
                PreviousPage = (bool)pagination["previous_page"],
                CurrentPage = (int)pagination["current_page"],
                TotalNumberPages = (int)pagination["total_number_pages"],
            };
            model.Users = new List<UserViewModel>();
            foreach (var user in body["users"])
            {
                model.Users.Add(
                    new UserViewModel
                    {
                        Id_User = (int)user["id_user"],
                        Name = user["name"].ToString(),
                        Email = user["email"].ToString(),
                        Permission = (int)user["permission"],
                        Address = user["address"].ToString(),
                        District = user["district"].ToString(),
                        City = user["city"].ToString(),
                        State = user["state"].ToString(),
                        ZipCode = user["zipcode"].ToString(),
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
                Id_User = (int)body["id_user"],
                Name = body["name"].ToString(),
                Email = body["email"].ToString(),
                Permission = (int)body["permission"],
                Address = body["address"].ToString(),
                District = body["district"].ToString(),
                City = body["city"].ToString(),
                State = body["state"].ToString(),
                ZipCode = body["zipcode"].ToString(),
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
                Id_User = (int)body["id_user"],
                Name = body["name"].ToString(),
                Email = body["email"].ToString(),
                Permission = (int)body["permission"],
                Address = body["address"].ToString(),
                District = body["district"].ToString(),
                City = body["city"].ToString(),
                State = body["state"].ToString(),
                ZipCode = body["zipcode"].ToString(),
            };

            return View(model);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(UserViewModel model)
        {
            WSRequest request = new WSRequest("/users/" + model.Id_User);
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("email", model.Email),
                    new KeyValuePair<string, string>("permission", model.Permission.ToString()),
                    new KeyValuePair<string, string>("address", model.Address),
                    new KeyValuePair<string, string>("district", model.District ),
                    new KeyValuePair<string, string>("city", model.City ),
                    new KeyValuePair<string, string>("state", model.State ),
                    new KeyValuePair<string, string>("zipcode", model.ZipCode ),
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