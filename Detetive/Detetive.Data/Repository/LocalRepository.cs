using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Detetive.Data.Repository
{
    public class LocalRepository : BaseRepository, ILocalRepository
    {
        public LocalRepository() : base()
        {
        }

        public List<Local> Listar()
        {
            return this.Context.Locais.AsNoTracking().Where(_ => _.Ativo).ToList();
        }

        public Local Obter(int idLocal)
        {
            return this.Context.Locais.AsNoTracking().Single(_ => _.Id == idLocal && _.Ativo);
        }
    }
}