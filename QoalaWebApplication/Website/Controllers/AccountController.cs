using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Website.Models.BO;
using Website.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;

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

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            new AccountsBO().doLogin(model);
            return View(model);
        }
        
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            IEnumerable<KeyValuePair<string, string>> Iuser = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("name", model.Name),
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };


            // bool result = new AccountsBO().doRegister(Iuser);

            using (HttpClient client = new HttpClient())
            {

                HttpContent q = new FormUrlEncodedContent(query);

                string url = "http://ws.qoala.com.br/accounts/register";

                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;

                        Console.WriteLine(mycontent);

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Home");

        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Index", "Home");
        }
        
    }
}