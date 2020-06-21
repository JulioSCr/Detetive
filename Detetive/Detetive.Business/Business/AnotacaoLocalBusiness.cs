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
        private readonly ILocalBusiness _localBusiness;
        private readonly IAnotacaoLocalRepository _anotacaoLocalRepository;

        public AnotacaoLocalBusiness(ILocalBusiness localBusiness,
                                        IAnotacaoLocalRepository anotacaoLocalRepository)
        {
            _localBusiness = localBusiness;
            _anotacaoLocalRepository = anotacaoLocalRepository;
        }

        public void CriarAnotacoes(int idJogadorSala)
        {
            var locais = _localBusiness.Listar();
            var anotacoesLocais = _anotacaoLocalRepository.Listar(idJogadorSala);

            if (anotacoesLocais != null && anotacoesLocais.Any())
            {
                // Mantem apenas os locais que ainda não foram cadastrada nas anotações no Jogador na sala.
                locais = locais.Where(local => !anotacoesLocais.Any(anotacao => anotacao.IdLocal == local.Id)).ToList();
            }

            // Adiciona os locais que ainda não foram cadastradas.
            locais.ForEach(local => Adicionar(local.Id, idJogadorSala));
        }

        private AnotacaoLocal Adicionar(int idLocal, int idJogadorSala)
        {
            return _anotacaoLocalRepository.Adicionar(new AnotacaoLocal(idLocal, idJogadorSala));
        }

        public List<AnotacaoLocal> Listar(int idJogadorSala)
        {
            var lista = _anotacaoLocalRepository.Listar(idJogadorSala);

            lista.ForEach(_ => _.Local = _localBusiness.Obter(_.IdLocal));

            return lista;
        }

        public AnotacaoLocal Alterar(int idLocal, int idJogadorSala, bool valor)
        {
            return _anotacaoLocalRepository.Marcar(idLocal, idJogadorSala, valor);
        }
    }
}