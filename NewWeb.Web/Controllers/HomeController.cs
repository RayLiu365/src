using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace NewWeb.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : NewWebControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}