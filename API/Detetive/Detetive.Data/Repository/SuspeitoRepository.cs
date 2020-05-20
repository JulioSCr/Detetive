using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Detetive.Data.Repository
{
    public class SuspeitoRepository : ISuspeitoRepository
    {
        private readonly DetetiveContext Context;
        public SuspeitoRepository(DetetiveContext context)
        {
            this.Context = context;
        }

        public List<Suspeito> Listar()
        {
            return this.Context.Suspeitos.ToList();
        }
    }
}
