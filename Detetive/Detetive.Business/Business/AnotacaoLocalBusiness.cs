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
        private readonly IAnotacaoLocalRepository _anotacaoLocalRepository;

        public AnotacaoLocalBusiness(IAnotacaoLocalRepository anotacaoLocalRepository)
        {
            _anotacaoLocalRepository = anotacaoLocalRepository;
        }

        public AnotacaoLocal Adicionar(int idLocal, int idJogadorSala)
        {
            return _anotacaoLocalRepository.Adicionar(new AnotacaoLocal(idLocal, idJogadorSala));
        }

        public List<AnotacaoLocal> Listar()
        {
            return _anotacaoLocalRepository.Listar();
        }
    }
}
