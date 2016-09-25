using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Principal;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        public UserPrincipal currentUser { get; set; }
    }
}