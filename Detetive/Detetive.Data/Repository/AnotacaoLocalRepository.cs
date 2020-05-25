using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository
{
    public class AnotacaoLocalRepository : IAnotacaoLocalRepository
    {
        private readonly DetetiveContext Context;

        public AnotacaoLocalRepository()
        {
            this.Context = new DetetiveContext();
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

        public List<AnotacaoLocal> Listar()
        {
            return this.Context.AnotacaoLocais.AsNoTracking().Where(_ => _.Ativo).ToList();
        }
    }
}
