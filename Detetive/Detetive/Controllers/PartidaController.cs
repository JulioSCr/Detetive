using AutoMapper;
using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class PartidaController : Controller
    {
        private readonly ILocalBusiness _localBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IMovimentacaoBusiness _movimentacaoBusiness;

        public PartidaController(ILocalBusiness localBusiness,
                                 IJogadorSalaBusiness jogadorSalaBusiness,
                                 IMovimentacaoBusiness movimentacaoBusiness)
        {
            _localBusiness = localBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _movimentacaoBusiness = movimentacaoBusiness;
        }

        public ActionResult Manter()
        {
            return View();
        }

        public ActionResult Jogar(int idSala)
        {
            /// TO DO
            /// Deve retornar o ID_JOGADOR do jogador principal
            ViewBag.ID_JOGADOR_SALA = 1;

            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
            ViewBag.JogadoresSuspeitos = Mapper.Map<List<JogadorSala>, List<JogadorSuspeitoViewModel>>(jogadoresSala);

            var locais = _localBusiness.Listar();
            ViewBag.Locais = Mapper.Map<List<Local>, List<LocalViewModel>>(locais);

            return View();
        }

        /// <summary>Valida o movimento do jogador no tabuleiro.</summary>
        /// <param name="idJogadorSala" type="int">ID do JoggadorSala.</param>
        /// <param name="linha" type="int">Número da linha.</param>
        /// <param name="coluna" type="int">Número da coluna.</param>
        /// <returns type="Void"></returns>
        public string Mover(int idJogadorSala, int linha, int coluna)
        {
            return JsonConvert.SerializeObject(_movimentacaoBusiness.MoverJogador(idJogadorSala, linha, coluna));
        }

        public string Posicionar()
        {
            /// TO DO
            /// Deve retornar uma lista dos jogadores da sala
            /// e a posição de cada.
            /// O object deve ser substituído por uma view model

            /// Cordenada
            /// Varrer a lista de locais
            /// Retorna esse local
            /// idLocal
            /// Listar Locais --> pega id 
            /// Via Razor 


            //TabuleiroViewModel lTabuleiro;
            return JsonConvert.SerializeObject("");
        }

        public string MapearTabuleiro()
        {
            /// TO DO
            /// Deve retornar um objeto conforme o utilizado no javascript Scripts/Views/Partida/Jogar.js linha 220 a 342

            return JsonConvert.SerializeObject("");
        }
    }
}
