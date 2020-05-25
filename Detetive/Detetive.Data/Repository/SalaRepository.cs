using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Detetive.Data.Repository
{
    public class SalaRepository : ISalaRepository
    {
        private readonly DetetiveContext Context;
        public SalaRepository()
        {
            this.Context = new DetetiveContext();
        }

        public List<Sala> Listar()
        {
            return this.Context.Salas.ToList();
        }
    }
}