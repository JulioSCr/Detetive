using AutoMapper;
using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class SuspeitoController : Controller
    {
        private readonly ISuspeitoBusiness _suspeitoBusiness;

        public SuspeitoController(ISuspeitoBusiness suspeitoBusiness)
        {
            _suspeitoBusiness = suspeitoBusiness;
        }

        public ActionResult Listar()
        {
            ViewBag.Suspeitos = Mapper.Map<List<Suspeito>, List<SuspeitoViewModel>>(_suspeitoBusiness.Listar());

            return View();
        }

        public ActionResult Teste()
        {
            return View();
        }
    }
}