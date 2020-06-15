﻿using Detetive.Business.Business.Interfaces;
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
            /// TO DO
            /// Deve retornar o ID_JOGADOR do jogador principal
            ViewBag.ID_JOGADOR_SALA = 1;
            /// TO DO
            /// Deve retornar o id dos ID_JOGADOR de acordo com o suspeito que o jogador possuir
            ViewBag.JogadorSalaReitor = 1;
            ViewBag.JogadorSalaDiretora = 2;
            ViewBag.JogadorSalaProfessora = 3;
            ViewBag.JogadorSalaEstudante = 4;
            ViewBag.JogadorSalaZelador = 5;
            ViewBag.JogadorSalaPolicial = 6;
            ViewBag.JogadorSalaReporter = 7;
            ViewBag.JogadorSalaBibliotecaria = 8;
            /// TO DO
            /// Deve retornar os ID's de cada local
            ViewBag.IDPredioA = 1;
            ViewBag.IDPredioB = 2;
            ViewBag.IDSantiago = 3;
            ViewBag.IDPraca = 4;
            ViewBag.IDEtesp = 5;
            ViewBag.IDCantinaAB = 6;
            ViewBag.IDCA = 7;
            ViewBag.IDAuditorio = 8;
            ViewBag.IDGinasio = 9;
            return View();
        }

        /// <summary>Valida o movimento do jogador no tabuleiro.</summary>
        /// <param name="idJogadorSala" type="int">ID do JoggadorSala.</param>
        /// <param name="linha" type="int">Número da linha.</param>
        /// <param name="coluna" type="int">Número da coluna.</param>
        /// <returns type="Void"></returns>
        public bool Mover(int idJogadorSala, int linha, int coluna)
        {
            return _movimentacaoBusiness.MoverJogador(idJogadorSala, linha, coluna);
        }


        /// <summary>
        /// Obtem a posição atual de cada jogador da sala
        /// </summary>
        /// <returns>Retorna uma lista com ID-JOGADOR_SALA e a sua posição atual</returns>
        public string GetPosicaoAtual()
        {
            /// TO DO
            /// Em caso de dúvida olhar o objeto de retorno em ~/Scripts/Views/Partida/Jogar.js
            return JsonConvert.SerializeObject("");
        }

        public string MapearTabuleiro()
        {
            /// TO DO
            /// Deve retornar um objeto conforme o utilizado no javascript Scripts/Views/Partida/Jogar.js linha 220 a 342

            return JsonConvert.SerializeObject("");
        }

        [HttpGet]
        public ActionResult ModalPalpite()
        {
            return PartialView();
        }

        /// <summary>
        /// Valida o palpite
        /// </summary>
        /// <param name="idJogadorSala"></param>
        /// <param name="idArma"></param>
        /// <param name="idLocal"></param>
        /// <returns></returns>
        public string Palpite(int idJogadorSala, int idArma, int idLocal)
        {
            /// Valida o palpite

            return JsonConvert.SerializeObject("");
        }
    }
}
