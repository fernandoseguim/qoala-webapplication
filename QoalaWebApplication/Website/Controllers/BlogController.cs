using System.Collections.Generic;
using System.Web.Mvc;
using Website.API;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [Route("blog/{page}")]
        public ActionResult Index(int page = 1)
        {
            WSRequest request = new WSRequest("posts?pageNumber=" + page);
            var response = request.Get();
            if(response.Code != 200)
            {
                ViewBag.posts = null;
            } else
            {
                List<PostViewModel> list = new List<PostViewModel>();
                foreach (var post in response.Body.GetValue("posts"))
                {
                    list.Add(new PostViewModel(post));
                }
                ViewBag.posts = list;
            }
            return View();
        }
    }
}