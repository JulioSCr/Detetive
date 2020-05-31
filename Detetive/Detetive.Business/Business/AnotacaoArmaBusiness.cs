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
        private readonly IAnotacaoArmaRepository _anotacaoArmaRepository;

        public AnotacaoArmaBusiness(IAnotacaoArmaRepository anotacaoArmaRepository)
        {
            _anotacaoArmaRepository = anotacaoArmaRepository;
        }

        public AnotacaoArma Adicionar(int idArma, int idJogadorSala)
        {
            return _anotacaoArmaRepository.Adicionar(new AnotacaoArma(idArma, idJogadorSala));
        }

        public List<AnotacaoArma> Listar()
        {
            return _anotacaoArmaRepository.Listar();
        }

        public AnotacaoArma Marcar(int idJogadorSala, int idArma, bool valor)
        {
            return _anotacaoArmaRepository.Marcar(idJogadorSala, idArma, valor);
        }
    }
}