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
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;

        public MovimentacaoBusiness(ILocalBusiness localBusiness,
                                    IJogadorSalaBusiness jogadorSalaBusiness)
        {
            _localBusiness = localBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
        }

        public Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            ///Cenário #01 - Entrar em um local
            ///Dado um jogador
            ///Quando ele está fora de um local e ele passa a ter a coordenada de uma porta de um local
            ///Então ele é movido para o local através do Id
            ///
            ///Cenário #02 - Sair de um local
            ///Dado um jogador
            ///Quando ele está dentro de um local e se movimenta na direção de uma das portas
            ///Então o sistema deve validar se a direção corresponde com a direção de uma das portas (através das coordenadas de origem e destino)
            ///se a direção corresponder a uma das portas então o sistema deve retornar id do local nulo e coordenada das portas igual a porta
            ///
            ///Cenário #03 - Movimentar pelas ruas
            ///Dado um jogador
            ///Quando em uma rua e se movendo para outra rua
            ///Então deve apenas validar se a coordenada não corresponde a um local e se ele está fora de um local
            ///
            var jogador = _jogadorSalaBusiness.Obter(idJogadorSala);

            if (jogador == default)
                return new Operacao("Jogador não encotrado.", false);

            if (!jogador.MinhaVez())
                return new Operacao("Não está na vez desse jogador.", false);

            if (!jogador.PossoMeMovimentar(novaCoordenadaLinha, novaCoordenadaColuna))
                return new Operacao("Não há movimentos suficientes para ir ao destino desejado.", false);

            var operacao = ValidarMovimento(jogador.CoordenadaLinha, jogador.CoordenadaColuna, novaCoordenadaLinha, novaCoordenadaColuna);

            if (operacao.Status)
                _jogadorSalaBusiness.Mover(jogador, novaCoordenadaLinha, novaCoordenadaColuna);

            return operacao;
        }

        private Operacao ValidarMovimento(int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna)
        {
            var locais = _localBusiness.Listar();

            foreach (var local in locais)
            {
                if (!local.DentroLocal(coordenadaOrigemLinha, coordenadaOrigemColuna) &&
                        local.DentroLocal(coordenadaDestinoLinha, coordenadaDestinoColuna))
                {
                    if (!local.PortaLocal(coordenadaDestinoLinha, coordenadaDestinoColuna))
                        return new Operacao($"Não é possível entre no(a) {local.Descricao} por esse quadrado.", false);
                }
            }

            return new Operacao("Operação válida.");
        }
    }
}