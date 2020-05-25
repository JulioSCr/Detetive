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
    public class AnotacaoSuspeitoRepository : IAnotacaoSuspeitoRepository
    {
        private readonly DetetiveContext Context;

        public AnotacaoSuspeitoRepository()
        {
            this.Context = new DetetiveContext();
        }

        public AnotacaoSuspeito Adicionar(AnotacaoSuspeito anotacao)
        {
            if (anotacao != default)
            {
                this.Context.AnotacaoSuspeitos.Add(anotacao);
                this.Context.SaveChangesAsync();
            }

            return anotacao;
        }

        public List<AnotacaoSuspeito> Listar()
        {
            return this.Context.AnotacaoSuspeitos.AsNoTracking().Where(_ => _.Ativo).ToList();
        }
    }
}
