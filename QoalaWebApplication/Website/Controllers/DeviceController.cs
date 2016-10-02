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
        public ActionResult Index()
        {
            WSRequest request = new WSRequest("devices/");
            request.AddAuthorization(Session["token"].ToString());
            var response = request.Get();

            if (response.Code != 200)
                return RedirectToAction("Index", "Profile");

            var body = response.Body;

            List<DeviceViewModel> model = new List<DeviceViewModel>();
            foreach (var device in body["devices"])
            {
                model.Add(
                    new DeviceViewModel
                    {
                        IdDevice = (int)device["id_device"],
                        Alarm = (bool)device["alarm"],
                        Alias = device["alias"].ToString(),
                        Color = device["color"].ToString(),
                        FrequencyUpdate = (int)device["frequency_update"],
                        IdUser = (int)device["id_user"]
                    }
                );
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
                return RedirectToAction("Index", "Device");

            var body = response.Body;

            var model = new DeviceViewModel
            {
                IdDevice = (int)body["id_device"],
                Alarm = (bool)body["alarm"],
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
                return RedirectToAction("Index", "Device");

            var body = response.Body;

            var model = new DeviceViewModel
            {
                IdDevice = (int)body["id_device"],
                //Alarm = (bool)body["alarm"],
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

            return RedirectToAction("Index", "Device", new { message = "O device " + device.IdDevice + " foi atualizado" });
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

            return RedirectToAction("Index", "Device", new { message = "O device " + device.IdDevice + " foi atualizado" });
        }
    }
}