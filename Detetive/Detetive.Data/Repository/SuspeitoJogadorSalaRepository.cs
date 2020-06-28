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
    public class SuspeitoJogadorSalaRepository : BaseRepository, ISuspeitoJogadorSalaRepository
    {
        public SuspeitoJogadorSalaRepository() : base()
        {
        }

        public SuspeitoJogadorSala Adicionar(SuspeitoJogadorSala suspeitoJogadorSala)
        {
            if (suspeitoJogadorSala != default)
            {
                this.Context.SuspeitosJogadorSala.Add(suspeitoJogadorSala);
                this.Context.SaveChanges();
            }

            return suspeitoJogadorSala;
        }

        public List<SuspeitoJogadorSala> Listar(int idJogadorSala)
        {
            return this.Context.SuspeitosJogadorSala.AsNoTracking().Where(_ => _.IdJogadorSala == idJogadorSala && _.Ativo).ToList();
        }

        public SuspeitoJogadorSala Obter(int idSuspeito, int idJogadorSala)
        {
            return this.Context.SuspeitosJogadorSala.AsNoTracking().SingleOrDefault(_ => _.IdJogadorSala == idJogadorSala && _.IdSuspeito == idSuspeito && _.Ativo);
        }
    }
}
