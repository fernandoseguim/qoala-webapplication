using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Website.Models.BO;
using Website.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Text;
using Website.API;
using System.Web;

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

            AccountsBO account = AccountsBO.Instance();

            string r = account.doLogin(model);

            Session["token"] = r;

            //Session["token"]  = result.
            //var token = Session["token"];

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