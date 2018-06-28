using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewWeb.Web.Controllers
{
    public class BackendTasksController : Controller
    {
        // GET: BackendTasks
        public ActionResult Index()
        {
            return View();
        }
    }
}