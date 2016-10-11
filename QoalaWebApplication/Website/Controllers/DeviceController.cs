using System.Collections.Generic;
using System.Web.Mvc;
using Website.Controllers.ActionFilters;
using Website.Models.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class DeviceController : Controller
    {
        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Index(int page = 1)
        {
            WSRequest request = new WSRequest("devices/?page=" + page);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Profile", new { message = "Não foi possível buscar os devices" });

            var body = response.Body;

            DeviceListViewModel model = new DeviceListViewModel();
            var pagination = body.GetValue("pagination");
            model.Pagination = new PaginationViewModel
            {
                NextPage = (bool) pagination["next_page"],
                PreviousPage = (bool) pagination["previous_page"],
                CurrentPage = (int) pagination["current_page"],
                TotalNumberPages = (int) pagination["total_number_pages"],
            };
            model.Devices = new List<DeviceViewModel>();
            foreach (var device in body["devices"])
            {
                model.Devices.Add(new DeviceViewModel
                {
                    IdDevice = (int)device["id_device"],
                    //Alarm = (bool)device["alarm"],
                    Alias = device["alias"].ToString(),
                    Color = device["color"].ToString(),
                    FrequencyUpdate = (int)device["frequency_update"],
                    IdUser = (int)device["id_user"]
                });
            };
            
            return View(model);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Show(int idUser, int idDevice)
        {
            WSRequest request = new WSRequest("/users/" + idUser + "/devices/" + idDevice);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Profile", new { message = "Não foi possível buscar o device" });

            var body = response.Body;

            var model = new DeviceViewModel
            {
                IdDevice = (int)body["id_device"],
                Alias = body["alias"].ToString(),
                Color = body["color"].ToString(),
                FrequencyUpdate = (int)body["frequency_update"],
                IdUser = (int)body["id_user"]
            };

            return View(model);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult Edit(int idUser, int idDevice)
        {
            WSRequest request = new WSRequest("/users/" + idUser + "/devices/" + idDevice);
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Home", new { message = "Não foi possível buscar o device" });

            var body = response.Body;

            var model = new DeviceViewModel
            {
                IdDevice = (int)body["id_device"],
                Alias = body["alias"].ToString(),
                Color = body["color"].ToString(),
                FrequencyUpdate = (int)body["frequency_update"],
                IdUser = (int)body["id_user"]
            };

            return View(model);
        }


        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Update(DeviceViewModel device)
        {
            WSRequest request = new WSRequest("/users/" + device.IdUser + "/devices/" + device.IdDevice);
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("alias", device.Alias.ToString()),
                    new KeyValuePair<string, string>("color", device.Color.ToString()),
                    new KeyValuePair<string, string>("frequency_update", device.FrequencyUpdate.ToString())
                };

            request.AddJsonParameter(parameters);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Device", device);

            return RedirectToAction("Show", "Device", device);
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult TurnAlarm(DeviceViewModel device)
        {
            WSRequest request = new WSRequest("/users/" + device.IdUser + "/devices/" + device.IdDevice + "/turn_alarm");
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("alarm", (!device.Alarm).ToString())
                };

            request.AddJsonParameter(parameters);
            request.AddAuthorization(Session["token"].ToString());

            var response = request.Put();

            if (response.Code != 204)
                return RedirectToAction("Edit", "Device", device);

            return RedirectToAction("Show", "Device", device);
        }

        [HttpGet]
        [AuthorizationRequest]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Create(DeviceViewModel device)
        {
            var user = (UserViewModel)Session["CurrentUser"];
            WSRequest request = new WSRequest("/users/" + user.Id_User + "/devices");
            request.AddAuthorization(Session["token"].ToString());
            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("color", device.Color),
                    new KeyValuePair<string, string>("alias", device.Alias),
                    new KeyValuePair<string, string>("frequency_update", device.FrequencyUpdate.ToString()),
                    new KeyValuePair<string, string>("alarm", true.ToString())
                };

            request.AddJsonParameter(parameters);
            var response = request.Post();

            if (response.Code != 201)
                return RedirectToAction("Index", "Device", new { message = "O device não foi cadastrado" });

            return RedirectToAction("Index", "Device", new { message = "O device foi cadastrado" });
        }


        [HttpPost]
        [AuthorizationRequest]
        public ActionResult Delete(int idUser, int idDevice)
        {
            WSRequest request = new WSRequest("/users/" + idUser + "/devices/" + idDevice);
            request.AddAuthorization(Session["token"].ToString());
            
            var response = request.Delete();

            if (response.Code != 204)
                return RedirectToAction("Index", "Device", new { message = "O device não foi deletado" });

            return RedirectToAction("Index", "Device", new { message = "O device foi deletado" });
        }
    }
}