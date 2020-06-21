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
    public class SuspeitoRepository : BaseRepository, ISuspeitoRepository
    {
        public SuspeitoRepository() : base()
        {
        }

        public List<Suspeito> Listar()
        {
            return this.Context.Suspeitos.AsNoTracking().Where(_ => _.Ativo).ToList();
        }

        public Suspeito Obter(int idSuspeito)
        {
            return this.Context.Suspeitos.AsNoTracking().SingleOrDefault(_ => _.Id == idSuspeito && _.Ativo);
        }
    }
}