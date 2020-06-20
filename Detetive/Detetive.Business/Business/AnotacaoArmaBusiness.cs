using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class AnotacaoArmaBusiness : IAnotacaoArmaBusiness
    {
        private readonly IArmaBusiness _armaBusiness;
        private readonly IAnotacaoArmaRepository _anotacaoArmaRepository;

        public AnotacaoArmaBusiness(IArmaBusiness armaBusiness,
                                    IAnotacaoArmaRepository anotacaoArmaRepository)
        {
            _armaBusiness = armaBusiness;
            _anotacaoArmaRepository = anotacaoArmaRepository;
        }

        public void CriarAnotacoes(int idJogadorSala)
        {
            var armas = _armaBusiness.Listar();
            var anotacoesArmas = _anotacaoArmaRepository.Listar(idJogadorSala);

            if (anotacoesArmas != null && anotacoesArmas.Any())
            {
                // Mantem apenas as armas que ainda não foram cadastrada nas anotações no Jogador na sala.
                armas = armas.Where(arma => !anotacoesArmas.Any(anotacao => anotacao.IdArma == arma.Id)).ToList();
            }

            // Adiciona as armas que ainda não foram cadastradas.
            armas.ForEach(arma => Adicionar(arma.Id, idJogadorSala));
        }

        private AnotacaoArma Adicionar(int idArma, int idJogadorSala)
        {
            return _anotacaoArmaRepository.Adicionar(new AnotacaoArma(idArma, idJogadorSala));
        }

        public List<AnotacaoArma> Listar(int idJogadorSala)
        {
            var lista = _anotacaoArmaRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Arma = _armaBusiness.Obter(_.IdArma));

            return lista;
        }

        public AnotacaoArma Alterar(int idArma, int idJogadorSala, bool valor)
        {
            return _anotacaoArmaRepository.Marcar(idArma, idJogadorSala, valor);
        }
    }
}