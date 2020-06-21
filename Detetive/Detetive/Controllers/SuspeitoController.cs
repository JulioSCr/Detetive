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
        private readonly ISalaBusiness _salaBusiness;
        private readonly IJogadorBusiness _jogadorBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;

        public SuspeitoController(ISalaBusiness salaBusiness,
                                  IJogadorBusiness jogadorBusiness,
                                  ISuspeitoBusiness suspeitoBusiness,
                                  IJogadorSalaBusiness jogadorSalaBusiness)
        {
            _salaBusiness = salaBusiness;
            _jogadorBusiness = jogadorBusiness;
            _suspeitoBusiness = suspeitoBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
        }

        public ActionResult Listar(int idSala, int idJogadorSala)
        {
            var sala = _salaBusiness.Obter(idSala);

            if (sala == default)
                return RedirectToAction("Manter", "Sala", new Operacao("Sala não encontrada.", false));

            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            
            if (jogadorSala == default)
                return RedirectToAction("Manter", "Sala", new Operacao("Jogador não encontrada.", false));

            var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);

            ViewBag.Sala_ID = sala.Id;
            ViewBag.ID_JOGADOR_SALA = idJogadorSala;
            ViewBag.NomeJogador = jogador.Descricao;
            ViewBag.Suspeitos = Mapper.Map<List<Suspeito>, List<SuspeitoViewModel>>(_suspeitoBusiness.Listar());

            return View();
        }

        public ActionResult Teste()
        {
            return View();
        }
    }
}