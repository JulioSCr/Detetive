﻿using AutoMapper;
using Detetive.Business.Business;
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
        private readonly IPartidaBusiness _partidaBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;
        private readonly IHistoricoBusiness _historicoBusiness;

        private readonly IArmaJogadorSalaBusiness _armaJogadorSalaBusiness;
        private readonly ISuspeitoJogadorSalaBusiness _suspeitoJogadorSalaBusiness;
        private readonly ILocalJogadorSalaBusiness _localJogadorSalaBusiness;

        public PartidaController(ILocalBusiness localBusiness,
                                 IPartidaBusiness partidaBusiness,
                                 IPortaLocalBusiness portaLocalBusiness,
                                 IJogadorSalaBusiness jogadorSalaBusiness,
                                 IAnotacaoArmaBusiness anotacaoArmaBusiness,
                                 IAnotacaoLocalBusiness anotacaoLocalBusiness,
                                 IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness,
                                 IHistoricoBusiness historicoBusiness,
                                 IArmaJogadorSalaBusiness armaJogadorSalaBusiness,
                                 ISuspeitoJogadorSalaBusiness suspeitoJogadorSalaBusiness,
                                 ILocalJogadorSalaBusiness localJogadorSalaBusiness)
        {
            _localBusiness = localBusiness;
            _partidaBusiness = partidaBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
            _historicoBusiness = historicoBusiness;
            _armaJogadorSalaBusiness = armaJogadorSalaBusiness;
            _suspeitoJogadorSalaBusiness = suspeitoJogadorSalaBusiness;
            _localJogadorSalaBusiness = localJogadorSalaBusiness;
        }

        public ActionResult Manter()
        {
            return View();
        }

        public ActionResult Jogar(int idJogadorSala)
        {
            /// Cenário carregar partida
            /// Dada: 
            /// uma partida
            /// Quando: 
            /// tela da partida for carregada
            /// Então: 
            /// todos os jogadores serão posicionados ok
            /// as cartas de cada jogador deverão ser listadas
            /// as anotações dos jogadores deverão estar checadas
            /// o chat deve ser recarregado

            //int idJogadorSala = 20;

            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            int idSala = jogadorSala.IdSala;

            var operacao = _partidaBusiness.Iniciar(idSala);

            ViewBag.ID_Sala = idSala;

            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
            ViewBag.JogadoresSuspeitos = Mapper.Map<List<JogadorSala>, List<JogadorSuspeitoViewModel>>(jogadoresSala);

            var locais = _localBusiness.Listar();
            ViewBag.Locais = Mapper.Map<List<Local>, List<LocalViewModel>>(locais);

            this.CarregarAnotacoes(jogadorSala.Id);
            this.CarregarCartas(jogadorSala.Id);

            var jogadorSalaViewModel = Mapper.Map<JogadorSala, JogadorSalaViewModel>(_jogadorSalaBusiness.Obter(idJogadorSala));

            ViewBag.JogadorSala = jogadorSalaViewModel;

            if (jogadorSalaViewModel.Posicao.IdLocal == 1 || jogadorSalaViewModel.Posicao.IdLocal == 7 || jogadorSalaViewModel.Posicao.IdLocal == 8 || jogadorSalaViewModel.Posicao.IdLocal == 6)
            {
                ViewBag.passagemSecreta = true;
            }
            else
            {
                ViewBag.passagemSecreta = false;
            }

            return View();
        }

        /// <summary>Valida o movimento do jogador no tabuleiro.</summary>
        /// <param name="idJogadorSala" type="int">ID do JoggadorSala.</param>
        /// <param name="linha" type="int">Número da linha.</param>
        /// <param name="coluna" type="int">Número da coluna.</param>
        /// <returns type="Void"></returns>
        [HttpPost]
        public string MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            var operacao = _partidaBusiness.MoverJogador(idJogadorSala, novaCoordenadaLinha, novaCoordenadaColuna);

            if (!operacao.Status)
            {
                return JsonConvert.SerializeObject(operacao);
            }

            var jogadorSalaViewModel = Mapper.Map<JogadorSala, JogadorSalaViewModel>(_jogadorSalaBusiness.Obter(idJogadorSala));
            return JsonConvert.SerializeObject(new Operacao(JsonConvert.SerializeObject(jogadorSalaViewModel)));
        }


        /// <summary>
        /// Obtem a posição atual de cada jogador da sala
        /// </summary>
        /// <returns> Retorna uma lista com ID-JOGADOR_SALA e a sua posição atual. </returns>
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

        public string CarregarCartas(int idJogadorSala)
        {
            // Remover linha abaixo quando finalizar o desenvolvimento.
            idJogadorSala = 251;

            try
            {
                var armas = _armaJogadorSalaBusiness.Listar(idJogadorSala);
                var suspeitos = _suspeitoJogadorSalaBusiness.Listar(idJogadorSala);
                var locais = _localJogadorSalaBusiness.Listar(idJogadorSala);

                List<string> caminhoImageCartas = new List<string>();

                if (armas != null && armas.Any())
                    caminhoImageCartas.AddRange(armas.Select(_ => _.Arma.UrlImagem).ToList());

                if (suspeitos != null && suspeitos.Any())
                    caminhoImageCartas.AddRange(suspeitos.Select(_ => _.Suspeito.UrlImagem).ToList());

                if (locais != null && locais.Any())
                    caminhoImageCartas.AddRange(locais.Select(_ => _.Local.UrlImagem).ToList());

                return JsonConvert.SerializeObject(new Operacao(JsonConvert.SerializeObject(caminhoImageCartas)));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao($"Ocorreu um problema: {ex.Message}", false));
            }
        }

        [HttpGet]
        public ActionResult ModalPalpite()
        {
            // To Do
            ViewBag.Armas = null;
            ViewBag.Suspeitos = null;
            return PartialView();
        }

        [HttpGet]
        public ActionResult ModalAcusar()
        {
            // To Do
            ViewBag.Armas = null;
            ViewBag.Suspeitos = null;
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
            return JsonConvert.SerializeObject(_partidaBusiness.Palpitar(idSala, idJogadorSala, idLocal, idSuspeito, idArma));
        }

        public string Acusar(int idJogadorSala, int idSala, int idArma, int idLocal, int idSuspeito)
        {
            return JsonConvert.SerializeObject(_partidaBusiness.Acusar(idSala, idJogadorSala, idLocal, idSuspeito, idArma));
        }

        public string HistoricoPartida(int idSala)
        {
            return JsonConvert.SerializeObject(Mapper.Map<List<Historico>, List<HistoricoViewModel>>(_historicoBusiness.Listar(idSala).OrderBy(_ => _.DataCriacao).ToList()));
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
