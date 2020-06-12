using Detetive.Business.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    [Route("partida/")]
    public class PartidaController : Controller
    {
        private readonly IMovimentacaoBusiness _movimentacaoBusiness;

        public PartidaController(IMovimentacaoBusiness movimentacaoBusiness)
        {
            _movimentacaoBusiness = movimentacaoBusiness;
        }

        public ActionResult Manter()
        {
            return View();
        }

        public ActionResult Jogar()
        {
            ViewBag.ID_JOGADOR_SALA = 1;
            return View();
        }

        [HttpPut]
        [Route("movimentar/{idJogadorSala}")]
        public bool Mover(int idJogadorSala, int linha, int coluna)
        {
            return _movimentacaoBusiness.MoverJogador(idJogadorSala, linha, coluna);
        }
    }
}
