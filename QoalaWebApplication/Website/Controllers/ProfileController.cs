using System;
using System.Web.Mvc;
using Website.API;
using Website.Controllers.ActionFilters;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        [AuthorizationActionFilter]
        public ActionResult Index()
        {
            WSRequest request = new WSRequest("accounts/me");
            request.AddAuthorization(Session["token"].ToString());

            try
            {
                var response = request.Post();
                if (response.Code != 200)
                    return RedirectToAction("Index", "Home", new { Message = "Algum erro aconteceu" });

                var body = response.Body;

                ViewBag.Name = body.GetValue("name");
                ViewBag.Email = body.GetValue("email");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home", new { Message = e.Message });
            }
            
            return View();
        }
    }
}