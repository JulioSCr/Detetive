using Detetive.Business.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISuspeitoRepository _suspeitoRepository;
        
        public HomeController(ISuspeitoRepository suspeitoRepository)
        {
            _suspeitoRepository = suspeitoRepository;
        }

        public ActionResult Index()
        {
            var lista = _suspeitoRepository.Listar();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}