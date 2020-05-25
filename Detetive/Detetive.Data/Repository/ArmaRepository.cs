using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Detetive.Data.Repository
{
    public class ArmaRepository : IArmaRepository
    {
        private readonly DetetiveContext Context;
        public ArmaRepository()
        {
            this.Context = new DetetiveContext();
        }

        public List<Arma> Listar()
        {
            return this.Context.Armas.ToList();
        }
    }
}