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

            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
            var jogadorSala = jogadoresSala.SingleOrDefault(_ => _.Id == idJogadorSala);

            if (jogadorSala == default || jogadorSala.IdSala != sala.Id)
                return RedirectToAction("Manter", "Sala", new Operacao("Jogador não encontrada.", false));

            var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);

            ViewBag.Sala_ID = sala.Id;
            ViewBag.ID_JOGADOR_SALA = idJogadorSala;
            ViewBag.NomeJogador = jogador.Descricao;

            var suspeitosViewModel = Mapper.Map<List<Suspeito>, List<SuspeitoViewModel>>(_suspeitoBusiness.Listar());
            suspeitosViewModel.ForEach(suspeitoViewModel =>
            {
                jogadorSala = jogadoresSala.FirstOrDefault(_ => _.IdSuspeito == suspeitoViewModel.Id);

                if (jogadorSala != default)
                {
                    suspeitoViewModel.IdJogadorSala = jogadorSala.Id;
                    suspeitoViewModel.NickJogador = jogadorSala != null ? _jogadorBusiness.Obter(jogadorSala.IdJogador).Descricao : "";
                }
            });

            ViewBag.Suspeitos = suspeitosViewModel;

            return View();
        }

        [HttpPost]
        public string SelecionarSuspeito(int idSala, int idJogadorSala, int idSuspeito)
        {
            try
            {
                string descricaoSuspeito = "";
                var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

                if (jogadorSala.IdSuspeito != null)
                {
                    int idSuspeitoDesconsiderado = jogadorSala.IdSuspeito ?? 0;
                    if (idSuspeitoDesconsiderado > 0)
                        descricaoSuspeito = _suspeitoBusiness.Obter(idSuspeitoDesconsiderado).Descricao;
                }

                _jogadorSalaBusiness.SelecionarSuspeito(idSala, idJogadorSala, idSuspeito);

                var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);

                var retorno = Json(new
                {
                    DescricaoJogador = jogador.Descricao,
                    DescricaoSuspeito = descricaoSuspeito
                }, "json");

                var operacao = new Operacao(JsonConvert.SerializeObject(retorno), true);

                return JsonConvert.SerializeObject(operacao);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao(ex.Message, false));
            }
        }

        [HttpPost]
        public string DesconsiderarSuspeito(int idJogadorSala)
        {
            try
            {
                var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

                if (jogadorSala == default)
                    return JsonConvert.SerializeObject(new Operacao("Jogador não encontrado!", false));

                if (!jogadorSala.IdSuspeito.HasValue)
                    return JsonConvert.SerializeObject(new Operacao("Jogador ainda não selecionou nenhum jogador.", false));

                var suspeito = _suspeitoBusiness.Obter(jogadorSala.IdSuspeito.GetValueOrDefault());

                var retorno = new
                {
                    DescricaoSuspeito = suspeito.Descricao
                };

                jogadorSala.AlterarSuspeito(null);
                _jogadorSalaBusiness.Alterar(jogadorSala);

                return JsonConvert.SerializeObject(new Operacao(JsonConvert.SerializeObject(retorno), true));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao(ex.Message, false));
            }
        }
    }
}