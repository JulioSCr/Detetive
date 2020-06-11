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
    public class AnotacaoLocalBusiness : IAnotacaoLocalBusiness
    {
        private readonly ILocalRepository _localRepository;
        private readonly IAnotacaoLocalRepository _anotacaoLocalRepository;

        public AnotacaoLocalBusiness(ILocalRepository localRepository,
                                        IAnotacaoLocalRepository anotacaoLocalRepository)
        {
            _localRepository = localRepository;
            _anotacaoLocalRepository = anotacaoLocalRepository;
        }

        public AnotacaoLocal Adicionar(int idLocal, int idJogadorSala)
        {
            AnotacaoLocal anotacaoLocal = _anotacaoLocalRepository.Adicionar(new AnotacaoLocal(idLocal, idJogadorSala));

            return anotacaoLocal;
        }

        public List<AnotacaoLocal> Listar(int idJogadorSala)
        {
            var lista = _anotacaoLocalRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Local = _localRepository.Obter(_.IdLocal));

            return lista;
        }

        public AnotacaoLocal Marcar(int idJogadorSala, int idLocal, bool valor)
        {
            return _anotacaoLocalRepository.Marcar(idJogadorSala, idLocal, valor);
        }
    }
}
