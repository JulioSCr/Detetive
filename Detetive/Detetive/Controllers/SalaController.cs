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
        private readonly ICrimeBusiness _crimeBusiness;

        public SalaController(ISalaBusiness salaBusiness, IJogadorBusiness jogadorBusiness, IJogadorSalaBusiness jogadorSalaBusiness, ICrimeBusiness crimeBusiness)
        {
            _salaBusiness = salaBusiness;
            _jogadorBusiness = jogadorBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _crimeBusiness = crimeBusiness;
        }

        public ActionResult Manter()
        {
            return View();
        }

        [HttpPost]
        public string Ingressar(int idSala, string dsJogador)
        {
            try
            {
                var crime = _crimeBusiness.Obter(idSala);
                if (crime != null)
                    return JsonConvert.SerializeObject(new Operacao("A partida já foi iniciada.", false));

                var jogador = _jogadorBusiness.Adicionar(dsJogador);
                var sala = _salaBusiness.Obter(idSala);

                if (sala == default)
                    return JsonConvert.SerializeObject(new Operacao("Sala não encontrada.", false));

                var operacao = _jogadorSalaBusiness.Adicionar(sala, jogador.Id);

                if (!operacao.Status)
                    return JsonConvert.SerializeObject(operacao);

                var jogadorSala = _jogadorSalaBusiness.Obter(jogador.Id, sala.Id);
                
                sala.AlterarJogador(jogadorSala.Id);
                _salaBusiness.Alterar(sala);

                var retorno = Json(new { idSala = sala.Id, idJogadorSala = jogadorSala.Id }, "json");

                return JsonConvert.SerializeObject(new Operacao(JsonConvert.SerializeObject(retorno)));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao(ex.Message, false));
            }
        }

        public int CriarSala()
        {
            var sala = _salaBusiness.Adicionar();

            return sala.Id;
        }
    }
}