using System;
using System.Web.Mvc;
using Website.Models;
using System.Collections.Generic;
using Website.API;

namespace Website.Controllers
{
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            WSRequest request = new WSRequest("accounts/login");

            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            request.AddJsonParameter(parameters);

            try
            {
                var response = request.Post();
                if (response.Code != 201)
                {
                    ModelState.AddModelError(
                        "",
                        response.Body.GetValue("Message").ToString()
                    );
                    return View(model);
                }
                string token = response.Body.GetValue("token").ToString();
                Session["token"] = token;
            } catch(Exception e) {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            WSRequest request = new WSRequest("accounts/register");

            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            request.AddJsonParameter(parameters);

            try
            {
                var response = request.Post();
                if (response.Code != 201)
                {
                    ModelState.AddModelError(
                        "",
                        response.Body.GetValue("Message").ToString()
                    );
                    return View(model);
                }
                string token = response.Body.GetValue("token").ToString();
                Session["token"] = token;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Logout()
        {
            WSRequest request = new WSRequest("accounts/logout");

            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("token", Session["token"].ToString())
                };

            request.AddAuthorization(Session["token"].ToString());
            request.AddJsonParameter(parameters);

            try
            {
                var response = request.Post();
                if (response.Code != 200)
                {
                    return RedirectToAction("Index", "Home", new { Message = "Não foi possível delogar" });
                }
                Session["token"] = null;
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home", new { Message = e.Message });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}