using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class PartidaBusiness : IPartidaBusiness
    {
        private readonly ISalaBusiness _salaBusiness;
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IArmaBusiness _armaBusiness;
        private readonly ILocalBusiness _localBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;

        public PartidaBusiness(ISalaBusiness salaBusiness,
                               ICrimeBusiness crimeBusiness,
                               IPortaLocalBusiness portaLocalBusiness,
                               IJogadorSalaBusiness jogadorSalaBusiness,
                               IArmaBusiness armaBusiness,
                               ILocalBusiness localBusiness,
                               ISuspeitoBusiness suspeitoBusiness)
        {
            _salaBusiness = salaBusiness;
            _crimeBusiness = crimeBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _armaBusiness = armaBusiness;
            _localBusiness = localBusiness;
            _suspeitoBusiness = suspeitoBusiness;
        }

        //public Operacao Iniciar(int idSala)
        //{
        //    var sala = _salaBusiness.Obter(idSala);
        //    if (sala == default)
        //        return new Operacao("Sala não encontrada.", false);

        //    var crimeSala = _crimeBusiness.Obter(idSala);
        //    if (crimeSala != default)
        //        return new Operacao("A sala já foi iniciada.", false);

        //    var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
        //    if (jogadoresSala == null || jogadoresSala.Count < 3)
        //        return new Operacao("Para iniciar a partida, é necessário que haja pelo menos 3 jogadores.", false);

        //    return IniciarPartida(sala);
        //}

        //private Operacao IniciarPartida(Sala sala)
        //{
        //    var armas = _armaBusiness.Listar();
        //    var locais = _localBusiness.Listar();
        //    var suspeitos = _suspeitoBusiness.Listar();

        //    if (armas == null || locais == null || suspeitos == null || !armas.Any() || !locais.Any() || !suspeitos.Any())
        //        return new Operacao("Ocorreu um problema ao carregar as cartas.", false);

        //    var crime = _crimeBusiness.Adicionar(sala);

        //}

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
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

            if (jogadorSala == default && jogadorSala.IdSala != idSala)
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
                _jogadorSalaBusiness.Alterar(jogadorSala);

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
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

            if (jogadorSala == default && jogadorSala.IdSala != idSala)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            this.MoverJogadorSalaParaLocal(idSuspeito, idSala, idLocal);

            //TODO
            return null;
        }

        private void MoverJogadorSalaParaLocal(int idSuspeito, int idSala, int idLocal)
        {
            var jogadorSala = _jogadorSalaBusiness.ObterPorSuspeito(idSuspeito, idSala);

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
            _jogadorSalaBusiness.Alterar(jogadorSala);
        }

        public Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

            if (jogadorSala == default)
                return new Operacao("Jogador não encotrado.", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (!jogadorSala.PossoMeMovimentar(novaCoordenadaLinha, novaCoordenadaColuna))
                return new Operacao("Não há movimentos suficientes para ir ao destino desejado.", false);

            var operacao = ValidarMovimento(jogadorSala.IdLocal, jogadorSala.CoordenadaLinha, jogadorSala.CoordenadaColuna, novaCoordenadaLinha, novaCoordenadaColuna);

            if (operacao.Status)
            {
                var porta = _portaLocalBusiness.Obter(novaCoordenadaLinha, novaCoordenadaColuna);
                jogadorSala.Mover(novaCoordenadaLinha, novaCoordenadaColuna, porta?.IdLocal);

                _jogadorSalaBusiness.Alterar(jogadorSala);
            }

            return operacao;
        }

        private Operacao ValidarMovimento(int? idLocal, int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna)
        {
            if (!idLocal.HasValue)
            {
                var local = _localBusiness.Obter(coordenadaDestinoLinha, coordenadaDestinoColuna);
                
                if (local != default && !local.DentroLocal(coordenadaOrigemLinha, coordenadaOrigemColuna) &&
                    local.DentroLocal(coordenadaDestinoLinha, coordenadaDestinoColuna) && !local.PortaLocal(coordenadaDestinoLinha, coordenadaDestinoColuna))
                {
                    return new Operacao("Não é possível entrar no local por esse quadrado.", false);
                }

                return new Operacao("Operação válida.");
            }
            else
            {
                var portas = _portaLocalBusiness.Listar(idLocal.Value);
                if (portas == null || !portas.Any())
                    return new Operacao("Portas da sala não cadastradas.", false);

                if (portas.Any(p => p.ValidarMovimento(coordenadaDestinoLinha, coordenadaDestinoColuna)))
                    return new Operacao("Operação válida.");

                return new Operacao("Não é possível sair do local por essa direção.", false);
            }
        }
    }
}