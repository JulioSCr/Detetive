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
    public class AnotacaoSuspeitoBusiness : IAnotacaoSuspeitoBusiness
    {
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IAnotacaoSuspeitoRepository _anotacaoSuspeitoRepository;
        
        public AnotacaoSuspeitoBusiness(ISuspeitoBusiness suspeitoBusiness, 
                                        IAnotacaoSuspeitoRepository anotacaoSuspeitoRepository)
        {
            _suspeitoBusiness = suspeitoBusiness;
            _anotacaoSuspeitoRepository = anotacaoSuspeitoRepository;
        }

        public void CriarAnotacoes(int idJogadorSala)
        {
            var suspeitos = _suspeitoBusiness.Listar();
            var anotacoesSuspeitos = _anotacaoSuspeitoRepository.Listar(idJogadorSala);

            if (anotacoesSuspeitos != null && anotacoesSuspeitos.Any())
            {
                // Mantem apenas os suspeitos que ainda não foram cadastrada nas anotações no Jogador na sala.
                suspeitos = suspeitos.Where(suspeito => !anotacoesSuspeitos.Any(anotacao => anotacao.IdSuspeito == suspeito.Id)).ToList();
            }

            // Adiciona os suspeitos que ainda não foram cadastradas.
            suspeitos.ForEach(suspeito => Adicionar(suspeito.Id, idJogadorSala));
        }

        public AnotacaoSuspeito Adicionar(int idSuspeito, int idJogadorSala)
        {
            return _anotacaoSuspeitoRepository.Adicionar(new AnotacaoSuspeito(idSuspeito, idJogadorSala));
        }

        public List<AnotacaoSuspeito> Listar(int idJogadorSala)
        {
            var lista = _anotacaoSuspeitoRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Suspeito = _suspeitoBusiness.Obter(_.IdSuspeito));

            return lista;
        }

        public AnotacaoSuspeito Alterar(int idSuspeito, int idJogadorSala, bool valor)
        {
            return _anotacaoSuspeitoRepository.Alterar(idSuspeito, idJogadorSala, valor);
        }       
    }
}