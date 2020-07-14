using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Business.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IJogadorBusiness _jogadorBusiness;
        private readonly IArmaBusiness _armaBusiness;
        private readonly ILocalBusiness _localBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IArmaJogadorSalaBusiness _armaJogadorSalaBusiness;
        private readonly ILocalJogadorSalaBusiness _localJogadorSalaBusiness;
        private readonly ISuspeitoJogadorSalaBusiness _suspeitoJogadorSalaBusiness;
        private readonly IHistoricoBusiness _historicoBusiness;
        private readonly IJogadorSalaRepository _jogadorSalaRepository;
        private readonly Dado _dado;

        public PartidaBusiness(ISalaBusiness salaBusiness,
                                ICrimeBusiness crimeBusiness,
                                IPortaLocalBusiness portaLocalBusiness,
                                IJogadorSalaBusiness jogadorSalaBusiness,
                                IArmaBusiness armaBusiness,
                                ILocalBusiness localBusiness,
                                ISuspeitoBusiness suspeitoBusiness,
                                IArmaJogadorSalaBusiness armaJogadorSalaBusiness,
                                ILocalJogadorSalaBusiness localJogadorSalaBusiness,
                                ISuspeitoJogadorSalaBusiness suspeitoJogadorSalaBusiness,
                                IHistoricoBusiness historicoBusiness,
                                IJogadorSalaRepository jogadorSalaRepository,
                                IJogadorBusiness jogadorBusiness, Dado dado)
        {
            _salaBusiness = salaBusiness;
            _crimeBusiness = crimeBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _armaBusiness = armaBusiness;
            _localBusiness = localBusiness;
            _suspeitoBusiness = suspeitoBusiness;
            _armaJogadorSalaBusiness = armaJogadorSalaBusiness;
            _localJogadorSalaBusiness = localJogadorSalaBusiness;
            _suspeitoJogadorSalaBusiness = suspeitoJogadorSalaBusiness;
            _historicoBusiness = historicoBusiness;
            _jogadorBusiness = jogadorBusiness;
            _jogadorSalaRepository = jogadorSalaRepository;
            _dado = dado;
        }

        public Operacao Iniciar(int idJogadorSala, int idSala)
        {
            var sala = _salaBusiness.Obter(idSala);
            if (sala == default)
                return new Operacao("Sala não encontrada.", false);

            var crimeSala = _crimeBusiness.Obter(idSala);
            if (crimeSala != default)
                return new Operacao("A sala já foi iniciada.", false);

            if (sala.IdJogadorSala != idJogadorSala)
                return new Operacao("Esse jogador não pode iniciar a partida", false);

            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
            if (jogadoresSala == null || jogadoresSala.Count < 3)
                return new Operacao("Para iniciar a partida, é necessário que haja pelo menos 3 jogadores.", false);

            return IniciarPartida(sala, jogadoresSala);
        }

        public Operacao RolarDados(int idJogadorSala, int idSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default && jogadorSala.IdSala != idSala)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.Jogando)
                return new Operacao("Jogador não está mais jogando", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (jogadorSala.RolouDados)
                return new Operacao("O jogador já rolou os dados.", false);

            var sala = _salaBusiness.Obter(jogadorSala.IdSala);
            if (sala == default)
                return new Operacao("sala não encotrada", false);

            var crime = _crimeBusiness.Obter(sala.Id);
            if (crime == default)
                return new Operacao("sala não encotrada", false);

            if (crime.Solucionado())
                return new Operacao("A partida acabou, o caso já foi solucionado", false);

            jogadorSala.AlterarQuantidadeMovimento(_dado.Rolar());
            var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);

            _historicoBusiness.Adicionar(new Historico(idSala, $"O jogador {jogador.Descricao} obteve {jogadorSala.QuantidadeMovimento} na rolagem dos dados."));
            _jogadorSalaBusiness.Alterar(jogadorSala);

            return new Operacao("Operação realizada com sucesso.");
        }

        private Operacao IniciarPartida(Sala sala, List<JogadorSala> jogadoresSala)
        {
            var armas = _armaBusiness.Listar();
            var locais = _localBusiness.Listar();
            var suspeitos = _suspeitoBusiness.Listar();

            if (armas == null || locais == null || suspeitos == null || !armas.Any() || !locais.Any() || !suspeitos.Any())
                return new Operacao("Ocorreu um problema ao carregar as cartas.", false);

            var crime = _crimeBusiness.Adicionar(sala);

            armas = armas.Where(a => a.Id != crime.IdArma).ToList();
            locais = locais.Where(l => l.Id != crime.IdLocal).ToList();
            suspeitos = suspeitos.Where(s => s.Id != crime.IdSuspeito).ToList();

            DistribuirCartasJogadores(jogadoresSala, armas, locais, suspeitos);
            jogadoresSala = DefinirOrdemJogadoresSalaETurnoInicial(jogadoresSala);

            var jogador = _jogadorBusiness.Obter(jogadoresSala.First(_ => _.VezJogador && _.Ativo).IdJogador);

            _historicoBusiness.Adicionar(new Historico(sala.Id, $"Partida #{sala.Id} Iniciada. O jogador {jogador.Descricao} inicia"));
            return new Operacao("Partida iniciada com sucesso!");
        }

        private List<JogadorSala> DefinirOrdemJogadoresSalaETurnoInicial(List<JogadorSala> jogadoresSala)
        {
            jogadoresSala = jogadoresSala.OrderBy(_ => Guid.NewGuid()).ToList();

            int i = 1;
            jogadoresSala.ForEach(jogadorSala =>
            {
                jogadorSala.NumeroOrdem = i++;
            });
            jogadoresSala.First(_ => _.NumeroOrdem == 1).VezJogador = true;

            return _jogadorSalaBusiness.Alterar(jogadoresSala);
        }

        private void DistribuirCartasJogadores(List<JogadorSala> jogadoresSala, List<Arma> armas, List<Local> locais, List<Suspeito> suspeitos)
        {
            jogadoresSala = jogadoresSala.OrderBy(_ => Guid.NewGuid()).ToList();

            while (armas.Any() || locais.Any() || suspeitos.Any())
            {
                foreach (var jogadorSala in jogadoresSala)
                {
                    Random sorteio = new Random();
                    if (armas.Any())
                    {
                        var index = sorteio.Next(armas.Count);
                        var arma = armas[index];

                        _armaJogadorSalaBusiness.Adicionar(arma.Id, jogadorSala.Id);
                        armas.RemoveAt(index);
                    }
                    else if (locais.Any())
                    {
                        var index = sorteio.Next(locais.Count);
                        var local = locais[index];

                        _localJogadorSalaBusiness.Adicionar(local.Id, jogadorSala.Id);
                        locais.RemoveAt(index);
                    }
                    else if (suspeitos.Any())
                    {
                        var index = sorteio.Next(suspeitos.Count);
                        var suspeito = suspeitos[index];

                        _suspeitoJogadorSalaBusiness.Adicionar(suspeito.Id, jogadorSala.Id);
                        suspeitos.RemoveAt(index);
                    }
                }
            }
        }

        public Operacao Finalizar(int idJogadorSala)
        {
            if (idJogadorSala <= 0)
                return new Operacao("Id do jogador não informado", false);

            return FinalizarTurno(idJogadorSala);
        }

        private Operacao FinalizarTurno(int idJogadorSala)
        {
            try
            {
                var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
                if (jogadorSala == default)
                    return new Operacao("Jogador não encontrado", false);

                if (!jogadorSala.Jogando)
                    return new Operacao("Jogador não está mais jogando", false);

                if (!jogadorSala.MinhaVez())
                    return new Operacao("Não está na vez desse jogador.", false);

                var sala = _salaBusiness.Obter(jogadorSala.IdSala);
                if (sala == default)
                    return new Operacao("sala não encotrada", false);

                var crime = _crimeBusiness.Obter(sala.Id);
                if (crime == default)
                    return new Operacao("sala não encotrada", false);

                if (crime.Solucionado())
                    return new Operacao("O crime já foi solucionado", false);

                string idProximoJogadorSala = this.AlteraVezJogadores(idJogadorSala);

                return new Operacao(idProximoJogadorSala);
            }
            catch (Exception ex)
            {
                return new Operacao(ex.Message, false);
            }
        }

        private string AlteraVezJogadores(int idJogadorSala)
        {
            // Altera termina a vez do jogador atual 
            var jogadorSalaAtual = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSalaAtual == default) return String.Empty;

            // Lista os jogadores da sala 
            var jogadores = _jogadorSalaBusiness.Listar(jogadorSalaAtual.IdSala).ToList();

            // Lógica para encontrar qual o próximo jogador 
            JogadorSala proximoJogadorSala = ObterProximoJogador(jogadorSalaAtual, jogadores);

            // Dá a vez para o próximo jogador
            jogadorSalaAtual.FinalizarTurno();
            proximoJogadorSala.IniciarTurno();

            _jogadorSalaBusiness.Alterar(jogadorSalaAtual);
            _jogadorSalaBusiness.Alterar(proximoJogadorSala);

            // Mensagem para o histórico
            var nickJogador = _jogadorBusiness.Obter(jogadorSalaAtual.IdJogador);
            var nickProximoJogador = _jogadorBusiness.Obter(proximoJogadorSala.IdJogador);

            _historicoBusiness.Adicionar(new Historico(proximoJogadorSala.IdSala, $"Player {nickJogador.Descricao} finalizou a rodada, {nickProximoJogador.Descricao}, é a sua vez!"));
            proximoJogadorSala.HabilitarPalpite();

            return Convert.ToString(proximoJogadorSala.Id);
        }

        private JogadorSala ObterProximoJogador(JogadorSala jogadorSalaAtual, List<JogadorSala> jogadores)
        {
            jogadores = jogadores.Where(_ => _.Jogando).ToList();

            if (jogadores.Any(_ => _.NumeroOrdem > jogadorSalaAtual.NumeroOrdem))
                return jogadores.OrderBy(_ => _.NumeroOrdem).First(_ => _.NumeroOrdem > jogadorSalaAtual.NumeroOrdem);
            else
                return jogadores.OrderBy(_ => _.NumeroOrdem).First();
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
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default && jogadorSala.IdSala != idSala)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.Jogando)
                return new Operacao("Jogador não está mais jogando", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            var crime = _crimeBusiness.Obter(idSala);
            if (crime == default)
                return new Operacao("Crime da sala informada não encontrado", false);

            if (crime.Solucionado())
                return new Operacao("A partida acabou, o caso já foi solucionado", false);

            var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);

            this.MoverJogadorSalaParaLocal(idSuspeito, idSala, idLocal);

            bool casoSolucionado = crime.ValidarAcusacaoCrime(idSuspeito, idArma, idLocal);
            if (casoSolucionado)
            {
                crime.AlterarJogadorSala(jogadorSala.Id);
                _crimeBusiness.Alterar(crime);

                _historicoBusiness.Adicionar(new Historico(idSala, $"Partida #{idSala} acabou. O jogador {jogador.Descricao} solucionou o caso."));
                return new Operacao("Caso Solucionado! Você é um verdadeiro Sherlock Holmes.");
            }
            else
            {
                jogadorSala.EncerrarParticipacao();
                _jogadorSalaBusiness.Alterar(jogadorSala);

                AlteraVezJogadores(jogadorSala.Id);

                _historicoBusiness.Adicionar(new Historico(idSala, $"O jogador {jogador.Descricao} errou a acusação e perdeu o jogo."));

                //Quando o jogador perde, suas cartas são distribuídas para o restantes dos jogadores
                DistribuirCartasJogador(jogadorSala);

                return new Operacao("Acusação errada! Você não é um Sherlock Holmes.");
            }
        }

        private void DistribuirCartasJogador(JogadorSala jogadorSala)
        {
            var jogadoresSala = _jogadorSalaBusiness.Listar(jogadorSala.IdSala).Where(_ => _.Jogando).ToList();

            var armasJogador = _armaJogadorSalaBusiness.Listar(jogadorSala.Id);
            var locaisJogador = _localJogadorSalaBusiness.Listar(jogadorSala.Id);
            var suspeitosJogador = _suspeitoJogadorSalaBusiness.Listar(jogadorSala.Id);

            _armaJogadorSalaBusiness.DesabilitarArmasJogador(jogadorSala.Id);
            _localJogadorSalaBusiness.DesabilitarLocaisJogador(jogadorSala.Id);
            _suspeitoJogadorSalaBusiness.DesabilitarSuspeitosJogador(jogadorSala.Id);

            var armas = armasJogador.Select(_ => _.Arma).ToList();
            var locais = locaisJogador.Select(_ => _.Local).ToList();
            var suspeitos = suspeitosJogador.Select(_ => _.Suspeito).ToList();

            DistribuirCartasJogadores(jogadoresSala, armas, locais, suspeitos);
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

            var jogadoresSala = _jogadorSalaBusiness.Listar(idSala);
            if (jogadoresSala.Count(_ => _.Jogando) <= 1)
                return new Operacao("Não é possível realizar mais palpites, pois há apenas 1 jogador", false);

            if (!jogadorSala.Jogando)
                return new Operacao("Jogador não está mais jogando", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (jogadorSala.RealizouPalpite)
                return new Operacao("Só é permitido realizar 1 palpite por rodada.", false);

            MoverJogadorSalaParaLocal(idSuspeito, idSala, idLocal);

            var armaPaupite = _armaBusiness.Obter(idArma);
            var localPaupite = _localBusiness.Obter(idLocal);
            var suspeitoPaupite = _suspeitoBusiness.Obter(idSuspeito);

            // Registra palpite no histórico da sala.
            var jogador = _jogadorBusiness.Obter(jogadorSala.IdJogador);
            _historicoBusiness.Adicionar(new Historico(idSala, $"O jogador {jogador.Descricao} palpitou as seguintes as cartas {armaPaupite.Descricao} (arma), {suspeitoPaupite.Descricao} (suspeito) e {localPaupite.Descricao} (local)"));

            jogadorSala.PalpiteRealizado();
            _jogadorSalaBusiness.Alterar(jogadorSala);

            // Ordena jogadores à esquerda do jogador
            var jogadoresSalaOrdenado = jogadoresSala.Where(_ => _.NumeroOrdem > jogadorSala.NumeroOrdem && _.Jogando)
                                                        .OrderBy(_ => _.NumeroOrdem).ToList();

            jogadoresSalaOrdenado.AddRange(jogadoresSala.Where(_ => _.NumeroOrdem < jogadorSala.NumeroOrdem && _.Jogando)
                                                        .OrderBy(_ => _.NumeroOrdem).ToList());

            foreach (var jogadorSalaEsquerda in jogadoresSalaOrdenado)
            {
                var arma = _armaJogadorSalaBusiness.Obter(idArma, jogadorSalaEsquerda.Id);
                var local = _localJogadorSalaBusiness.Obter(idLocal, jogadorSalaEsquerda.Id);
                var suspeito = _suspeitoJogadorSalaBusiness.Obter(idSuspeito, jogadorSalaEsquerda.Id);

                if (arma == default && local == default && suspeito == default)
                    continue;

                List<TipoCarta> tiposcarta = new List<TipoCarta>();

                if (arma != default)
                    tiposcarta.Add(TipoCarta.Arma);

                if (local != default)
                    tiposcarta.Add(TipoCarta.Local);

                if (suspeito != default)
                    tiposcarta.Add(TipoCarta.Suspeito);

                var jogadorComCarta = _jogadorBusiness.Obter(jogadorSalaEsquerda.IdJogador);
                _historicoBusiness.Adicionar(new Historico(idSala, $"O jogador {jogadorComCarta.Descricao} mostrou uma carta ao jogador {jogador.Descricao}"));

                // Sorteia a carta que que será revelado por conta do palpite
                switch (tiposcarta[new Random().Next(0, tiposcarta.Count)])
                {
                    case TipoCarta.Arma:
                        return new Operacao($"O jogador {jogadorComCarta.Descricao} lhe mostrou a carta {armaPaupite.Descricao}");

                    case TipoCarta.Local:
                        return new Operacao($"O jogador {jogadorComCarta.Descricao} lhe mostrou a carta {localPaupite.Descricao}");

                    case TipoCarta.Suspeito:
                        return new Operacao($"O jogador {jogadorComCarta.Descricao} lhe mostrou a carta {suspeitoPaupite.Descricao}");
                }
            }

            Historico historico = _historicoBusiness.Adicionar(new Historico(idSala, "Nenhum jogador possui as cartas palpitadas"));
            return new Operacao(historico.Descricao);
        }

        private void MoverJogadorSalaParaLocal(int idSuspeito, int idSala, int idLocal)
        {
            var jogadorSala = _jogadorSalaBusiness.ObterPorSuspeito(idSuspeito, idSala);
            if (jogadorSala == default)
                return;

            var local = _localBusiness.Obter(idLocal);
            if (local == default)
                return;

            jogadorSala.AlterarCoordenadas(jogadorSala.CoordenadaLinha, jogadorSala.CoordenadaColuna, idLocal);
            _jogadorSalaBusiness.Alterar(jogadorSala);
        }

        public Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default)
                return new Operacao("Jogador não encotrado.", false);

            if (!jogadorSala.Jogando)
                return new Operacao("Jogador não está mais jogando", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (!jogadorSala.PossoMeMovimentar())
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
                if (local != default && !local.DentroLocal(coordenadaOrigemLinha, coordenadaOrigemColuna) && local.DentroLocal(coordenadaDestinoLinha, coordenadaDestinoColuna) &&
                    !local.PortaLocal(coordenadaOrigemLinha, coordenadaOrigemColuna, coordenadaDestinoLinha, coordenadaDestinoColuna))
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

        public Operacao PassagemSecreta(int idJogadorSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default)
                return new Operacao("Jogador não encontrado", false);

            if (!jogadorSala.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (!jogadorSala.Jogando)
                return new Operacao("Jogador não está mais jogando", false);

            if (!jogadorSala.IdLocal.HasValue)
                return new Operacao("Essa operação não é válida na localização atual do jogador", false);

            var local = _localBusiness.Obter(jogadorSala.IdLocal.Value);
            if (local == default)
                return new Operacao("Local do jogador não encontrado", false);

            if (!local.IdLocalPassagemSecreta.HasValue)
                return new Operacao("Este local não possui passagem secreta", false);

            if (jogadorSala.PodeUtilizarPassagemSecreta())
                return new Operacao("O jogador não pode mais utilizar a passagem secreta", false);

            jogadorSala.AlterarCoordenadas(jogadorSala.CoordenadaLinha, jogadorSala.CoordenadaColuna, local.IdLocalPassagemSecreta.Value);
            jogadorSala.UtilizarPassagemSecreta();
            _jogadorSalaBusiness.Alterar(jogadorSala);

            return new Operacao($"{jogadorSala.IdLocal}");
        }
    }
}