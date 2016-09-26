using System.Web.Mvc;
using Website.Principal;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        private UserPrincipal _currentUser;
        public UserPrincipal currentUser 
        {
            get
            {
                return _currentUser;
            }
            set
            {
                ViewBag.currentUser = value;
                this._currentUser = value;
            }
        }
    }
}