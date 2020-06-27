using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class PropagandaController : Controller
    {
        // GET: Propaganda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ad()
        {
            return View();
        }
    }
}