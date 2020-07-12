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

            ViewBag.Sala = Mapper.Map<Sala, SalaViewModel>(sala);
            ViewBag.ID_JOGADOR_SALA = idJogadorSala;
            ViewBag.NomeJogador = jogador.Descricao;
            var jogadorSalaDono = _jogadorSalaBusiness.Obter(sala.IdJogadorSala.Value);
            ViewBag.NomeJogadorDono = _jogadorBusiness.Obter(jogadorSalaDono.IdJogador).Descricao;

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

            ViewBag.QtdeJogadorPronto = suspeitosViewModel.Where(x => x.IdJogadorSala != null).Count();

            ViewBag.Suspeitos = suspeitosViewModel;

            return View();
        }

        [HttpPost]
        public string SelecionarSuspeito(int idSala, int idJogadorSala, int idSuspeito)
        {
            try
            {
                var sala = _salaBusiness.Obter(idSala);
                if (sala == default)
                    return JsonConvert.SerializeObject(new Operacao("Sala não encontrada.", false));

                var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
                if (jogadorSala == default)
                    return JsonConvert.SerializeObject(new Operacao("Jogador Sala não encontrado.", false));

                var suspeito = _suspeitoBusiness.Obter(idSuspeito);
                if (suspeito == default)
                    return JsonConvert.SerializeObject(new Operacao("Suspeito não encontrado.", false));

                if (jogadorSala.IdSala != sala.Id)
                    return JsonConvert.SerializeObject(new Operacao("Jogador Sala não pertence a sala passada.", false));

                var suspeitoDesconsiderado = jogadorSala.IdSuspeito == null ? null : _suspeitoBusiness.Obter(jogadorSala.IdSuspeito.Value);

                jogadorSala.AlterarSuspeito(suspeito.Id);
                _jogadorSalaBusiness.Alterar(jogadorSala);

                var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);
                var retorno = Json(new
                {
                    DescricaoJogador = jogador.Descricao,
                    DescricaoSuspeitoSelecionado = suspeito.Descricao,
                    DescricaoSuspeitoDesconsiderado = suspeitoDesconsiderado == null ? "" : suspeitoDesconsiderado.Descricao
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