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
    public class LocalJogadorSalaRepository : BaseRepository, ILocalJogadorSalaRepository
    {
        public LocalJogadorSalaRepository() : base()
        {
        }

        public LocalJogadorSala Adicionar(LocalJogadorSala localJogadorSala)
        {
            if (localJogadorSala != default)
            {
                this.Context.LocalJogadorSala.Add(localJogadorSala);
                this.Context.SaveChanges();
            }

            return localJogadorSala;
        }

        public List<LocalJogadorSala> Listar(int idJogadorSala)
        {
            return this.Context.LocalJogadorSala.AsNoTracking().Where(_ => _.IdJogadorSala == idJogadorSala && _.Ativo).ToList();
        }

    }
}
