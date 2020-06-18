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
        private readonly IJogadorBusiness _jogadorBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;

        public SalaController(ISalaBusiness salaBusiness, IJogadorBusiness jogadorBusiness, IJogadorSalaBusiness jogadorSalaBusiness)
        {
            _salaBusiness = salaBusiness;
            _jogadorBusiness = jogadorBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
        }

        public void Manter(string dsJogador)
        {
            var jogador = _jogadorBusiness.Adicionar(dsJogador);

            var sala = _salaBusiness.Adicionar(jogador.Id);

            var operacao = _jogadorSalaBusiness.Adicionar(sala, jogador.Id);
        }

        public void Ingressar(int idSala, int idJogador)
        {
            var sala = _salaBusiness.Obter(idSala);

            if (sala == default)
                return;

            var operacao = _jogadorSalaBusiness.Adicionar(sala, idJogador);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class SalaController : Controller
    {
        // GET: ManterSala()
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manter()
        {
            return View();
        }
        public string CriarSala()
        {
            //object lRetorno = new
            //{
            //    IDSala = 1234,
            //    Status = "Ok"
            //};

            return "1234";
        }
    }
}