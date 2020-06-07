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
        private readonly IArmaRepository _armaRepository;
        private readonly IAnotacaoArmaRepository _anotacaoArmaRepository;

        public AnotacaoArmaBusiness(IArmaRepository armaRepository,
                                    IAnotacaoArmaRepository anotacaoArmaRepository)
        {
            _armaRepository = armaRepository;
            _anotacaoArmaRepository = anotacaoArmaRepository;
        }

        public AnotacaoArma Adicionar(int idArma, int idJogadorSala)
        {
            return _anotacaoArmaRepository.Adicionar(new AnotacaoArma(idArma, idJogadorSala));
        }

        public List<AnotacaoArma> Listar(int idJogadorSala)
        {
            var lista = _anotacaoArmaRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Arma = _armaRepository.Obter(_.IdArma));

            return lista;
        }

        public AnotacaoArma Marcar(int idJogadorSala, int idArma, bool valor)
        {
            return _anotacaoArmaRepository.Marcar(idJogadorSala, idArma, valor);
        }
    }
}