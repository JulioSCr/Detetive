﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class PopUpController : Controller
    {
        public ActionResult PopUp()
        {
            return PartialView();
        }
    }
}