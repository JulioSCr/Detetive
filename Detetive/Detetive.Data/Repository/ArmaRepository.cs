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
    public class ArmaRepository : BaseRepository, IArmaRepository
    {
        public ArmaRepository() : base()
        {
        }

        public List<Arma> Listar()
        {
            return this.Context.Armas.AsNoTracking().Where(_ => _.Ativo).ToList();
        }

        public Arma Obter(int idArma)
        {
            return this.Context.Armas.AsNoTracking().SingleOrDefault(_ => _.Id == idArma && _.Ativo);
        }
    }
}