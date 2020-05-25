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
    public class AnotacaoArmaRepository : IAnotacaoArmaRepository
    {
        private readonly DetetiveContext Context;

        public AnotacaoArmaRepository()
        {
            this.Context = new DetetiveContext();
        }

        public AnotacaoArma Adicionar(AnotacaoArma anotacao)
        {
            if (anotacao != default)
            {
                this.Context.AnotacaoArmas.Add(anotacao);
                this.Context.SaveChangesAsync();
            }

            return anotacao;
        }

        public List<AnotacaoArma> Listar()
        {
            return this.Context.AnotacaoArmas.AsNoTracking().Where(_ => _.Ativo).ToList();
        }
    }
}