using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository
{
    public class AnotacaoLocalRepository : BaseRepository, IAnotacaoLocalRepository
    {
        public AnotacaoLocalRepository() : base()
        {
        }

        public AnotacaoLocal Adicionar(AnotacaoLocal anotacao)
        {
            if (anotacao != default)
            {
                this.Context.AnotacaoLocais.Add(anotacao);
                this.Context.SaveChangesAsync();
            }

            return anotacao;
        }

        public List<AnotacaoLocal> Listar(int idJogadorSala)
        {
            return this.Context.AnotacaoLocais.AsNoTracking().Where(_ => _.IdJogadorSala == idJogadorSala && _.Ativo).ToList();
        }

        public AnotacaoLocal Marcar(int idLocal, int idJogadorSala, bool valor)
        {
            var anotacao = this.Context.AnotacaoLocais.Single(_ => _.IdJogadorSala == idJogadorSala && _.IdLocal == idLocal);

            anotacao.Sinalar(valor);
            this.Context.SaveChanges();

            return anotacao;
        }
    }
}
