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
    public class AnotacaoArmaRepository : BaseRepository, IAnotacaoArmaRepository
    {
        public AnotacaoArmaRepository() : base()
        {
        }

        public AnotacaoArma Adicionar(AnotacaoArma anotacao)
        {
            if (anotacao != default)
            {
                this.Context.AnotacaoArmas.Add(anotacao);
                this.Context.SaveChanges();
            }

            return anotacao;
        }

        public List<AnotacaoArma> Listar()
        {
            return this.Context.AnotacaoArmas.AsNoTracking().Where(_ => _.Ativo).ToList();
        }

        public AnotacaoArma Marcar(int idJogadorSala, int idArma, bool valor)
        {
            var anotacao = this.Context.AnotacaoArmas.Single(_ => _.IdJogadorSala == idJogadorSala && _.IdArma == idArma);

            anotacao.Marcado = valor;
            this.Context.SaveChanges();

            return anotacao;
        }
    }
}