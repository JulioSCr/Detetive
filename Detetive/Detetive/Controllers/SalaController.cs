using Detetive.Business.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class SalaController : Controller
    {
        private readonly ISalaBusiness _salaBusiness;

        public SalaController(ISalaBusiness salaBusiness)
        {
            _salaBusiness = salaBusiness;
        }

        public ActionResult Manter()
        {
            _salaBusiness.Criar(1);

            return View();
        }
    }
}