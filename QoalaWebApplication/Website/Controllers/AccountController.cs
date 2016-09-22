using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Website.Models;
using System.Collections.Generic;
using Website.API;

namespace Website.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        //
        // GET: /Account/Login
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

            IEnumerable<KeyValuePair<string, string>> login = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            request.AddJsonParameter(login);

            try
            {
                var response = request.Execute();
                if (response.Code != 201)
                {
                    ModelState.AddModelError(
                        "",
                        response.Body.GetValue("Message").ToString()
                    );
                    return View(model);
                }
                string token = response.Body.GetValue("Token").ToString();
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

            IEnumerable<KeyValuePair<string, string>> register = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            request.AddJsonParameter(register);

            try
            {
                var response = request.Execute();
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
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            return View();
        }
        
    }
}