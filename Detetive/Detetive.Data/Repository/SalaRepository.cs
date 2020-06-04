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
    public class SalaRepository : BaseRepository, ISalaRepository
    {
        public SalaRepository() : base()
        {
        }

        public List<Sala> Listar()
        {
            return this.Context.Salas.ToList();
        }
    }
}