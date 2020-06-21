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
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly ILocalBusiness _localBusiness;
        private readonly IJogadorBusiness _jogadorBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaRepository _jogadorSalaRepository;

        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;

        public JogadorSalaBusiness(ICrimeBusiness crimeBusiness, 
                                    ILocalBusiness localBusiness, 
                                    IJogadorBusiness jogadorBusiness, 
                                    ISuspeitoBusiness suspeitoBusiness, 
                                    IPortaLocalBusiness portaLocalBusiness, 
                                    IJogadorSalaRepository jogadorSalaRepository,
                                    IAnotacaoArmaBusiness anotacaoArmaBusiness,
                                    IAnotacaoLocalBusiness anotacaoLocalBusiness,
                                    IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness)
        {
            _crimeBusiness = crimeBusiness;
            _localBusiness = localBusiness;
            _jogadorBusiness = jogadorBusiness;
            _suspeitoBusiness = suspeitoBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaRepository = jogadorSalaRepository;
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
        }

        public JogadorSala Mover(JogadorSala jogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            jogadorSala.Mover(novaCoordenadaLinha, novaCoordenadaColuna);

            return _jogadorSalaRepository.Alterar(jogadorSala);
        }

        public JogadorSala Obter(int idJogadorSala)
        {
            return _jogadorSalaRepository.Obter(idJogadorSala);
        }

        public List<JogadorSala> Listar(int idSala)
        {
            var listaJogadores = _jogadorSalaRepository.Listar(idSala);
            var suspeitos = _suspeitoBusiness.Listar();

            listaJogadores.ForEach(x => x.Suspeito = suspeitos.FirstOrDefault(y => y.Id == x.IdSuspeito));

            return listaJogadores;
        }

        public Operacao Acusar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma)
        {
            if (idSala <= 0)
                return new Operacao("Id da sala não informado", false);

            if (idJogadorSala <= 0)
                return new Operacao("Id do jogador não informado", false);

            if (idLocal <= 0)
                return new Operacao("Id do local não informado", false);

            if (idSuspeito <= 0)
                return new Operacao("Id do suspeito não informado", false);

            if (idArma <= 0)
                return new Operacao("Id da arma não informada", false);

            return RealizarAcusacao(idSala, idJogadorSala, idLocal, idSuspeito, idArma);
        }

        private Operacao RealizarAcusacao(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma)
        {
            var jogadorSala = _jogadorSalaRepository.Obter(idJogadorSala, idSala);

            if (jogadorSala == default)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            var crime = _crimeBusiness.Obter(idSala);

            if (crime == default)
                return new Operacao("Crime da sala informada não encontrado", false);

            this.MoverJogadorSalaParaLocal(idSuspeito, idSala, idLocal);

            bool casoSolucionado = crime.ValidarAcusacaoCrime(idSuspeito, idArma, idLocal);

            if (casoSolucionado)
            {
                crime.AlterarJogadorSala(jogadorSala.Id);
                _crimeBusiness.Alterar(crime);

                return new Operacao("Caso Solucionado! Você é um verdadeiro Sherlock Holmes.");
            }
            else
            {
                jogadorSala.DefinirAtivo(false);
                _jogadorSalaRepository.Alterar(jogadorSala);

                return new Operacao("Acusação errada! Você não é um Sherlock Holmes.");
            }
        }

        public Operacao Palpitar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma)
        {
            if (idSala <= 0)
                return new Operacao("Id da sala não informado", false);

            if (idJogadorSala <= 0)
                return new Operacao("Id do jogador não informado", false);

            if (idLocal <= 0)
                return new Operacao("Id do local não informado", false);

            if (idSuspeito <= 0)
                return new Operacao("Id do suspeito não informado", false);

            if (idArma <= 0)
                return new Operacao("Id da arma não informada", false);

            return RealizarPalpite(idSala, idJogadorSala, idLocal, idSuspeito, idArma);
        }

        private Operacao RealizarPalpite(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma)
        {
            var jogadorSala = _jogadorSalaRepository.Obter(idJogadorSala, idSala);

            if (jogadorSala == default)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            this.MoverJogadorSalaParaLocal(idSuspeito, idSala, idLocal);

            //TODO
            return null;
        }

        private void MoverJogadorSalaParaLocal(int idSuspeito, int idSala, int idLocal)
        {
            var jogadorSala = _jogadorSalaRepository.ObterPorSuspeito(idSuspeito, idSala);

            if (jogadorSala == default)
                return;

            var local = _localBusiness.Obter(idLocal);

            if (local == default)
                return;

            var portas = _portaLocalBusiness.Listar(idLocal);

            if (portas != null && portas.Any())
                return;

            var porta = portas.First();

            jogadorSala.AlterarCoordenadas(porta.CoordenadaLinha, porta.CoordenadaColuna);
            _jogadorSalaRepository.Alterar(jogadorSala);
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

        public JogadorSala Obter(int idJogador, int idSala)
        {
            return _jogadorSalaRepository.Obter(idJogador, idSala);
        }

        public Operacao SelecionarSuspeito(int idSala, int idJogadorSala, int idSuspeito)
        {
            if (idSala <= 0)
                return new Operacao("Id da sala não informado", false);

            if (idJogadorSala <= 0)
                return new Operacao("Id do jogador não informado", false);

            if (idSuspeito <= 0)
                return new Operacao("Id do suspeito não informado", false);

            var jogadorSala = _jogadorSalaRepository.Obter(idJogadorSala);
            if (jogadorSala == default || jogadorSala.IdSala != idSala)
                return new Operacao("Jogador não encotrado.", false);

            var suspeito = _suspeitoBusiness.Obter(idSuspeito);
            if(suspeito == default)
                return new Operacao("Suspeito não encotrado.", false);

            jogadorSala.AlterarSuspeito(suspeito.Id);

            _jogadorSalaRepository.Alterar(jogadorSala);

            return new Operacao($"{suspeito.Descricao} selecionado com sucesso!");
        }

        private void GerarAnotacoesJogador(JogadorSala jogadorSala)
        {
            _anotacaoArmaBusiness.CriarAnotacoes(jogadorSala.Id);
            _anotacaoLocalBusiness.CriarAnotacoes(jogadorSala.Id);
            _anotacaoSuspeitoBusiness.CriarAnotacoes(jogadorSala.Id);
        }
    }
}