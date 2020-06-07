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
        private readonly ISuspeitoRepository _suspeitoRepository;
        private readonly IAnotacaoSuspeitoRepository _anotacaoSuspeitoRepository;
        
        public AnotacaoSuspeitoBusiness(ISuspeitoRepository suspeitoRepository, 
                                        IAnotacaoSuspeitoRepository anotacaoSuspeitoRepository)
        {
            _suspeitoRepository = suspeitoRepository;
            _anotacaoSuspeitoRepository = anotacaoSuspeitoRepository;
        }

        public AnotacaoSuspeito Adicionar(int idSuspeito, int idJogadorSala)
        {
            return _anotacaoSuspeitoRepository.Adicionar(new AnotacaoSuspeito(idSuspeito, idJogadorSala));
        }

        public List<AnotacaoSuspeito> Listar(int idJogadorSala)
        {
            var lista = _anotacaoSuspeitoRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Suspeito = _suspeitoRepository.Obter(_.IdSuspeito));

            return lista;
        }

        public AnotacaoSuspeito Marcar(int idJogadorSala, int idSuspeito, bool valor)
        {
            return _anotacaoSuspeitoRepository.Marcar(idJogadorSala, idSuspeito, valor);
        }
    }
}
