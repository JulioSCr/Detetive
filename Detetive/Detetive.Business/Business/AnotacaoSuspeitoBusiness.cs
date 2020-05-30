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
        private readonly IAnotacaoSuspeitoRepository _anotacaoSuspeitoRepository;
        
        public AnotacaoSuspeitoBusiness(IAnotacaoSuspeitoRepository anotacaoSuspeitoRepository)
        {
            _anotacaoSuspeitoRepository = anotacaoSuspeitoRepository;
        }

        public AnotacaoSuspeito Adicionar(int idSuspeito, int idJogadorSala)
        {
            return _anotacaoSuspeitoRepository.Adicionar(new AnotacaoSuspeito(idSuspeito, idJogadorSala));
        }

        public List<AnotacaoSuspeito> Listar()
        {
            return _anotacaoSuspeitoRepository.Listar();
        }
    }
}
