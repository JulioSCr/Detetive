using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class MovimentacaoBusiness : IMovimentacaoBusiness
    {
        private readonly ILocalBusiness _localBusiness;
        private readonly IPortaLocalBusiness _portaLocalBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;

        public MovimentacaoBusiness(ILocalBusiness localBusiness,
                                    IPortaLocalBusiness portaLocalBusiness,
                                    IJogadorSalaBusiness jogadorSalaBusiness)
        {
            _localBusiness = localBusiness;
            _portaLocalBusiness = portaLocalBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
        }

        public Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            var jogador = _jogadorSalaBusiness.Obter(idJogadorSala);

            if (jogador == default)
                return new Operacao("Jogador não encotrado.", false);

            if (!jogador.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (!jogador.PossoMeMovimentar(novaCoordenadaLinha, novaCoordenadaColuna))
                return new Operacao("Não há movimentos suficientes para ir ao destino desejado.", false);

            var operacao = ValidarMovimento(jogador.IdLocal, jogador.CoordenadaLinha, jogador.CoordenadaColuna, novaCoordenadaLinha, novaCoordenadaColuna);

            if (operacao.Status)
                _jogadorSalaBusiness.Mover(jogador, novaCoordenadaLinha, novaCoordenadaColuna);

            return operacao;
        }

        private Operacao ValidarMovimento(int? idLocal, int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna)
        {
            if (!idLocal.HasValue)
            {
                var locais = _localBusiness.Listar();

                foreach (var local in locais)
                {
                    // Valida entrada em um local.
                    if (!local.DentroLocal(coordenadaOrigemLinha, coordenadaOrigemColuna) &&
                            local.DentroLocal(coordenadaDestinoLinha, coordenadaDestinoColuna))
                    {
                        if (!local.PortaLocal(coordenadaDestinoLinha, coordenadaDestinoColuna))
                            return new Operacao($"Não é possível entre no(a) {local.Descricao} por esse quadrado.", false);
                    }
                }

                return new Operacao("Operação realizada com sucesso.");
            }
            else
            {
                var portas = _portaLocalBusiness.Listar(idLocal.Value);

                if (portas == null || !portas.Any())
                    return new Operacao("Portas da sala não cadastradas.", false);

                foreach (var porta in portas)
                {
                    if (porta.ValidarMovimento(coordenadaDestinoLinha, coordenadaDestinoColuna))
                        return new Operacao("Operação realizada com sucesso.");
                }

                return new Operacao("Não é possível sair do local por essa direção.", false);
            }
        }
    }
}