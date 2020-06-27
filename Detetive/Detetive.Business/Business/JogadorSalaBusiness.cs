using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class JogadorSalaBusiness : IJogadorSalaBusiness
    {
        private readonly IJogadorBusiness _jogadorBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IJogadorSalaRepository _jogadorSalaRepository;
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;

        public JogadorSalaBusiness(IJogadorBusiness jogadorBusiness, 
                                   ISuspeitoBusiness suspeitoBusiness, 
                                   IJogadorSalaRepository jogadorSalaRepository, 
                                   IAnotacaoArmaBusiness anotacaoArmaBusiness, 
                                   IAnotacaoLocalBusiness anotacaoLocalBusiness, 
                                   IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness)
        {
            _jogadorBusiness = jogadorBusiness;
            _suspeitoBusiness = suspeitoBusiness;
            _jogadorSalaRepository = jogadorSalaRepository;
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
        }

        public JogadorSala Obter(int idJogadorSala)
        {
            return _jogadorSalaRepository.Obter(idJogadorSala);
        }

        public JogadorSala Obter(int idJogador, int idSala)
        {
            return _jogadorSalaRepository.Obter(idJogador, idSala);
        }

        public JogadorSala ObterPorSuspeito(int idSuspeito, int idSala)
        {
            return _jogadorSalaRepository.ObterPorSuspeito(idSuspeito, idSala);
        }

        public Operacao Adicionar(Sala sala, int idJogador)
        {
            var jogadoresSala = _jogadorSalaRepository.Listar(sala.Id);

            if (jogadoresSala != default && jogadoresSala.Count >= 8)
                return new Operacao("A sala já está cheia", false);

            var jogador= _jogadorBusiness.Obter(idJogador);

            if (jogador == default)
                return new Operacao("Jogador não cadastrado", false);

            var jogadorSala = _jogadorSalaRepository.Adicionar(new JogadorSala(idJogador, sala.Id));

            GerarAnotacoesJogador(jogadorSala);

            return new Operacao("Jogador ingressado com sucesso!");
        }

        public JogadorSala Alterar(JogadorSala jogadorSala)
        {
            return _jogadorSalaRepository.Alterar(jogadorSala);
        }

        public List<JogadorSala> Listar(int idSala)
        {
            var listaJogadores = _jogadorSalaRepository.Listar(idSala);
            var suspeitos = _suspeitoBusiness.Listar();

            listaJogadores.ForEach(x => x.Suspeito = suspeitos.FirstOrDefault(y => y.Id == x.IdSuspeito));

            return listaJogadores;
        }

        private void GerarAnotacoesJogador(JogadorSala jogadorSala)
        {
            _anotacaoArmaBusiness.CriarAnotacoes(jogadorSala.Id);
            _anotacaoLocalBusiness.CriarAnotacoes(jogadorSala.Id);
            _anotacaoSuspeitoBusiness.CriarAnotacoes(jogadorSala.Id);
        }
    }
}