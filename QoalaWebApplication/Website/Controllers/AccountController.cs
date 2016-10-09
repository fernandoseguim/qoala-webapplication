using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Website.Models.API;
using Website.Models.ViewModels;
using System.Web;

namespace Website.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpPost]
        [Route("login")]
        public JsonResult Login(LoginViewModel model, string returnUrl)
        {
            WSRequest request = new WSRequest("accounts/login");

            IEnumerable<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            request.AddJsonParameter(parameters);
            var data = new
            {
                message = "Login efetuado com sucesso"
            }; 
            try
            {
                var response = request.Post();
                if (response.Code != 201)
                {
                    Response.StatusCode = 400;
                    data = new
                    {
                        message = response.Body.GetValue("Message").ToString()
                    };
                }
                else
                {
                    string token = response.Body.GetValue("token").ToString();
                    Session["token"] = token;

                    var cookie = new HttpCookie("qoala_token", token);
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }
            } catch(Exception e) {
                Response.StatusCode = 400;
                data = new
                {
                    message = e.Message
                };
            }

            return new JsonResult() { Data = data };
        }
        
        [HttpPost]
        [Route("register")]
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
            var data = new {
                message = "Registro efetuado com sucesso"
            };
            try
            {
                var response = request.Post();
                if (response.Code != 201)
                {
                    Response.StatusCode = 400;
                    data = new
                    {
                        message = response.Body.GetValue("Message").ToString()
                    };
                }
                else
                {
                    string token = response.Body.GetValue("token").ToString();
                    Session["token"] = token;
                    var cookie = new HttpCookie("qoala_token", token);
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                data = new
                {
                    message = e.Message
                };
            }

            return new JsonResult() { Data = data };
        }

        [HttpPost]
        [Route("logout")]
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
                Session["CurrentUser"] = null;
                var cookie = new HttpCookie("qoala_token");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home", new { Message = e.Message });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}