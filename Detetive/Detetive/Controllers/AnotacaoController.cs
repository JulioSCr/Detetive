using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoArmaRepository _anotacaoArmaRepository;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoLocalRepository _anotacaoLocalRepository;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;
        private readonly IAnotacaoSuspeitoRepository _anotacaoSuspeitoRepository;

        public AnotacaoController(IAnotacaoArmaBusiness anotacaoArmaBusiness, 
                                    IAnotacaoArmaRepository anotacaoArmaRepository, 
                                    IAnotacaoLocalBusiness anotacaoLocalBusiness, 
                                    IAnotacaoLocalRepository anotacaoLocalRepository, 
                                    IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness, 
                                    IAnotacaoSuspeitoRepository anotacaoSuspeitoRepository)
        {
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoArmaRepository = anotacaoArmaRepository;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoLocalRepository = anotacaoLocalRepository;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
            _anotacaoSuspeitoRepository = anotacaoSuspeitoRepository;
        }

        public ActionResult Index()
        {
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