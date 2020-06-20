using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using Newtonsoft.Json;
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

        public ActionResult Manter()
        {
            return View();
        }

        public string Ingressar(int idSala, string dsJogador)
        {
            var jogador = _jogadorBusiness.Adicionar(dsJogador);
            var sala = _salaBusiness.Obter(idSala);

            if (sala == default)
                return JsonConvert.SerializeObject(new Operacao("Sala não encontrada.", false)); 

            return JsonConvert.SerializeObject(_jogadorSalaBusiness.Adicionar(sala, jogador.Id));
        }

        public int CriarSala()
        {
            var sala = _salaBusiness.Adicionar();
            
            return sala.Id;
        }
    }
}