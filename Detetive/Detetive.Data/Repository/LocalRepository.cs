using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Detetive.Data.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private readonly DetetiveContext Context;
        public LocalRepository()
        {
            this.Context = new DetetiveContext();
        }

        public List<Local> Listar()
        {
            return this.Context.Locais.ToList();
        }
    }
}