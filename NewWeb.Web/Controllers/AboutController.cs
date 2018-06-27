using System.Web.Mvc;

namespace NewWeb.Web.Controllers
{
    public class AboutController : NewWebControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}