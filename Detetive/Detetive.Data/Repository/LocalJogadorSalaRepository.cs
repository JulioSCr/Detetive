using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Context;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                this.Context.LocaisJogadorSala.Add(localJogadorSala);
                this.Context.SaveChanges();
            }

            return localJogadorSala;
        }

        public List<LocalJogadorSala> Alterar(List<LocalJogadorSala> locais)
        {
            locais.ForEach(local => Context.Entry(local).State = EntityState.Modified);
            Context.SaveChanges();

            return locais;
        }

        public List<LocalJogadorSala> Listar(int idJogadorSala)
        {
            return this.Context.LocaisJogadorSala.AsNoTracking().Where(_ => _.IdJogadorSala == idJogadorSala && _.Ativo).ToList();
        }

        public LocalJogadorSala Obter(int idLocal, int idJogadorSala)
        {
            return this.Context.LocaisJogadorSala.AsNoTracking().SingleOrDefault(_ => _.IdJogadorSala == idJogadorSala && _.IdLocal == idLocal && _.Ativo);
        }
    }
}
