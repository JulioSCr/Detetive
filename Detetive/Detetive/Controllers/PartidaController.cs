using AutoMapper;
using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using Detetive.ViewModel.Anotacao;
using Detetive.ViewModel.Tabuleiro;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class PartidaController : Controller
    {
        private readonly ILocalBusiness _localBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IMovimentacaoBusiness _movimentacaoBusiness;
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;

        public PartidaController(ILocalBusiness localBusiness,
                                 IJogadorSalaBusiness jogadorSalaBusiness,
                                 IMovimentacaoBusiness movimentacaoBusiness,
                                 IAnotacaoArmaBusiness anotacaoArmaBusiness,
                                 IAnotacaoLocalBusiness anotacaoLocalBusiness,
                                 IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness,
                                 IPortaLocalBusiness portaLocalBusiness)
        {
            _localBusiness = localBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _movimentacaoBusiness = movimentacaoBusiness;
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
        }

        public ActionResult Manter()
        {
            return View();
        }

        public ActionResult Jogar(/*int idSala*/)
        {
            /// TO DO
            /// Deve retornar o ID_JOGADOR do jogador principal
            ViewBag.ID_JOGADOR_SALA = 1;

            var jogadoresSala = _jogadorSalaBusiness.Listar(1007);
            ViewBag.JogadoresSuspeitos = Mapper.Map<List<JogadorSala>, List<JogadorSuspeitoViewModel>>(jogadoresSala);

            var locais = _localBusiness.Listar();
            ViewBag.Locais = Mapper.Map<List<Local>, List<LocalViewModel>>(locais);

            // idJogadorSala
            this.CarregarAnotacoes(11);

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


        /// <summary>
        /// Obtem a posição atual de cada jogador da sala
        /// </summary>
        /// <returns>Retorna uma lista com ID-JOGADOR_SALA e a sua posição atual</returns>
        public string GetPosicaoAtual(/*int idSala*/)
        {
            int idSala = 1007;
            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);

            if (jogadoresSala == null || !jogadoresSala.Any())
                return String.Empty;

            var portasLocais = _portaLocalBusiness.Listar();
            var jogadoresViewModel = Mapper.Map<List<JogadorSala>, List<JogadorSalaViewModel>>(jogadoresSala);

            jogadoresViewModel = CarregarLocaisJogadoresViewModel(jogadoresViewModel, portasLocais);

            return JsonConvert.SerializeObject(jogadoresViewModel);
        }

        public string MapearTabuleiro()
        {
            /// TO DO
            /// Deve retornar um objeto conforme o utilizado no javascript Scripts/Views/Partida/Jogar.js linha 220 a 342
            var locais = _localBusiness.Listar();



            return JsonConvert.SerializeObject("");
        }

        [HttpGet]
        public ActionResult ModalPalpite()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult ModalAcusar()
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
        public string Palpite(int idJogadorSala, int idSala, int idArma, int idLocal, int idSuspeito)
        {
            return JsonConvert.SerializeObject(_jogadorSalaBusiness.Palpitar(idSala, idJogadorSala, idLocal, idSuspeito, idArma));
        }

        public string Acusar(int idJogadorSala, int idSala, int idArma, int idLocal, int idSuspeito)
        { 
            return JsonConvert.SerializeObject(_jogadorSalaBusiness.Acusar(idSala, idJogadorSala, idLocal, idSuspeito, idArma));
        }

        private void CarregarAnotacoes(int idJogadorSala)
        {
            ViewBag.AnotacaoArma = Mapper.Map<List<AnotacaoArma>, List<AnotacaoArmaViewModel>>(_anotacaoArmaBusiness.Listar(idJogadorSala));
            ViewBag.AnotacaoLocal = Mapper.Map<List<AnotacaoLocal>, List<AnotacaoLocalViewModel>>(_anotacaoLocalBusiness.Listar(idJogadorSala));
            ViewBag.AnotacaoSuspeito = Mapper.Map<List<AnotacaoSuspeito>, List<AnotacaoSuspeitoViewModel>>(_anotacaoSuspeitoBusiness.Listar(idJogadorSala));
        }

        private List<JogadorSalaViewModel> CarregarLocaisJogadoresViewModel(List<JogadorSalaViewModel> jogadoresViewModel, List<PortaLocal> portasLocais)
        {
            if (portasLocais == null || !portasLocais.Any())
                return jogadoresViewModel;

            jogadoresViewModel.ForEach(jogadorViewModel =>
            {
                var portaLocal = portasLocais.FirstOrDefault(_ => _.CoordenadaLinha == jogadorViewModel.Posicao.Linha &&
                                                                    _.CoordenadaColuna == jogadorViewModel.Posicao.Coluna);

                if (portaLocal != default)
                    jogadorViewModel.Posicao.IdLocal = portaLocal.IdLocal;
            });

            return jogadoresViewModel;
        }
    }
}
