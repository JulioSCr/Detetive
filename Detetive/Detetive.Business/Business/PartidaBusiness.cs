using Detetive.Business.Business.Interfaces;
using Detetive.Business.Entities;
using Detetive.Business.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class PartidaBusiness : IPartidaBusiness
    {
        private readonly ISalaBusiness _salaBusiness;
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly ILocalBusiness _localBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;

        public PartidaBusiness(ISalaBusiness salaBusiness,
                               ICrimeBusiness crimeBusiness,
                               ILocalBusiness localBusiness,
                               IPortaLocalBusiness portaLocalBusiness,
                               IJogadorSalaBusiness jogadorSalaBusiness)
        {
            _salaBusiness = salaBusiness;
            _crimeBusiness = crimeBusiness;
            _localBusiness = localBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
        }

        //public Operacao Iniciar(int idSala)
        //{
        //    var sala = _salaBusiness.Obter(idSala);
        //    if (sala == default)
        //        return new Operacao("Sala não encontrada.", false);

        //    var crime = _crimeBusiness.Obter(idSala);
        //    if (crime == default)
        //        return new Operacao("A sala já foi iniciada.", false);

        //    var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
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
            try
            {
                var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);

                if (jogadorSala == default)
                    return new Operacao("Jogador não encotrado.", false);

                if (!jogadorSala.MinhaVez())
                    return new Operacao("Não está na vez desse jogador.", false);

                if (!jogadorSala.PossoMeMovimentar(novaCoordenadaLinha, novaCoordenadaColuna))
                    return new Operacao("Não há movimentos suficientes para ir ao destino desejado.", false);

                var operacao = ValidarMovimento(jogadorSala.IdLocal, jogadorSala.CoordenadaLinha, jogadorSala.CoordenadaColuna, novaCoordenadaLinha, novaCoordenadaColuna);

                string direcao = DirecaoMovimento(jogadorSala.CoordenadaLinha, jogadorSala.CoordenadaColuna, novaCoordenadaLinha, novaCoordenadaColuna);
                if (jogadorSala.IdLocal.HasValue)
                {
                    var portas = _portaLocalBusiness.Listar(jogadorSala.IdLocal.Value);
                    if (portas == null || !portas.Any())
                    {
                        operacao.Retorno = "Porta não cadastrada.";
                        operacao.Status = false;
                    }
                    else
                    {
                        var porta = portas.Where(x => x.Direcao.Equals(direcao) && x.IdLocal == jogadorSala.IdLocal).FirstOrDefault();
                        if (porta == null)
                        {
                            operacao.Retorno = "Esta não é uma saída possível para este local, mova-se para outra direção.";
                            operacao.Status = false;
                        }
                        else
                        {
                            if (porta.Direcao == "direita")
                            {
                                novaCoordenadaLinha = porta.CoordenadaLinha;
                                novaCoordenadaColuna = porta.CoordenadaColuna + 1;
                            }
                            if (porta.Direcao == "esquerda")
                            {
                                novaCoordenadaLinha = porta.CoordenadaLinha;
                                novaCoordenadaColuna = porta.CoordenadaColuna - 1;
                            }
                            if (porta.Direcao == "baixo")
                            {
                                novaCoordenadaLinha = porta.CoordenadaLinha + 1;
                                novaCoordenadaColuna = porta.CoordenadaColuna;
                            }
                            if (porta.Direcao == "cima")
                            {
                                novaCoordenadaLinha = porta.CoordenadaLinha - 1;
                                novaCoordenadaColuna = porta.CoordenadaColuna;
                            }
                        }
                    }
                }

                if (operacao.Status)
                {
                    var porta = _portaLocalBusiness.Obter(novaCoordenadaLinha, novaCoordenadaColuna);
                    jogadorSala.Mover(novaCoordenadaLinha, novaCoordenadaColuna, porta?.IdLocal);

                    _jogadorSalaBusiness.Alterar(jogadorSala);
                }

                return operacao;
            }
            catch (Exception ex)
            {
                return new Operacao(ex.Message, false);
            }
        }

        private Operacao ValidarMovimento(int? idLocal, int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna)
        {
            if (!idLocal.HasValue)
            {
                var locais = _localBusiness.Listar();
                string direcao = DirecaoMovimento(coordenadaOrigemLinha, coordenadaOrigemColuna, coordenadaDestinoLinha, coordenadaDestinoColuna, true);
                if (locais.Any(l => !l.DentroLocal(coordenadaOrigemLinha, coordenadaOrigemColuna) && l.DentroLocal(coordenadaDestinoLinha, coordenadaDestinoColuna) &&
                                     !l.PortaLocal(coordenadaDestinoLinha, coordenadaDestinoColuna, direcao)))
                {
                    return new Operacao("Não é possível entrar no local por esse quadrado.", false);
                }

                return new Operacao("Operação válida.");
            }
            //else
            //{
            //    var portas = _portaLocalBusiness.Listar(idLocal.Value);
            //    if (portas == null || !portas.Any())
            //        return new Operacao("Portas da sala não cadastradas.", false);

            //    //if (portas.Any(p => p.ValidarMovimento(coordenadaDestinoLinha, coordenadaDestinoColuna)))
            //    //    return new Operacao("Operação válida.");

            //    return new Operacao("Não é possível sair do local por essa direção.", false);
            //}
            return new Operacao("Operação válida.");
        }

        public string DirecaoMovimento(int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna, bool oposta = false)
        {

            if (coordenadaOrigemLinha == coordenadaDestinoLinha && coordenadaOrigemColuna > coordenadaDestinoColuna)
            {
                if (oposta)
                    return "direita";
                return "esquerda";
            }
            if (coordenadaOrigemLinha == coordenadaDestinoLinha && coordenadaOrigemColuna < coordenadaDestinoColuna)
            {
                if (oposta)
                    return "esquerda";
                return "direita";
            }
            if (coordenadaOrigemLinha > coordenadaDestinoLinha && coordenadaOrigemColuna == coordenadaDestinoColuna)
            {
                if (oposta)
                    return "baixo";
                return "cima";
            }
            if (coordenadaOrigemLinha < coordenadaDestinoLinha && coordenadaOrigemColuna == coordenadaDestinoColuna)
            {
                if (oposta)
                    return "cima";
                return "baixo";
            }
            throw new InvalidOperationException("Direção do movimento não encontrada.");
        }
    }
}