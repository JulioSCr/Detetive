using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository
{
    public class HistoricoRepository : BaseRepository, IHistoricoRepository
    {
        public HistoricoRepository() : base()
        {

        }

        public Historico Adicionar(Historico historico)
        {
            if (historico != null)
            {
                this.Context.Historicos.Add(historico);
                this.Context.SaveChanges();
            }

            return historico;
        }

        public List<Historico> Listar(int idSala)
        {
            return this.Context.Historicos.AsNoTracking().Where(_ => _.IdSala == idSala && _.Ativo).ToList();
        }
    }
}