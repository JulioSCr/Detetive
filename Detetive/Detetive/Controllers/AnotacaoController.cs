using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    [Route("anotacao")]
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;

        public AnotacaoController(IAnotacaoArmaBusiness anotacaoArmaBusiness,
                                    IAnotacaoLocalBusiness anotacaoLocalBusiness,
                                    IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness)
        {
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
        }

        public ActionResult Anotacao()
        {
            var lista1 = _anotacaoArmaBusiness.Listar(1);
            var lista2 = _anotacaoLocalBusiness.Listar(1);
            var lista3 = _anotacaoSuspeitoBusiness.Listar(1);
            
            return View();
        }


        [HttpPut]
        [Route("arma/{idJogadorSala}/{id}/valor")]
        public ActionResult MarcarArma(int idJogadorSala, int id, bool valor)
        {
            try
            {
                var anotacao = _anotacaoArmaBusiness.Marcar(idJogadorSala, id, valor);

            }
            catch(Exception ex)
            {

            }

            // TODO
            return View();
        }

        [HttpPut]
        [Route("local/{idJogadorSala}/{id}/valor")]
        public ActionResult MarcarLocal(int idJogadorSala, int id, bool valor)
        {
            try
            {
                var anotacao = _anotacaoLocalBusiness.Marcar(idJogadorSala, id, valor);

            }
            catch (Exception ex)
            {

            }

            // TODO
            return View();
        }

        [HttpPut]
        [Route("suspeito/{idJogadorSala}/{id}/valor")]
        public ActionResult MarcarSuspeito(int idJogadorSala, int id, bool valor)
        {
            try
            {
                var anotacao = _anotacaoSuspeitoBusiness.Marcar(idJogadorSala, id, valor);

            }
            catch (Exception ex)
            {

            }

            // TODO
            return View();
        }
    }
}