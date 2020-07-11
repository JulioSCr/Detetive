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

        public Sala Adicionar(Sala sala)
        {
            this.Context.Salas.Add(sala);
            this.Context.SaveChanges();

            return sala;
        }

        public Sala Alterar(Sala sala)
        {
            if (sala != default)
            {
                var salaBD = this.Context.Salas.FirstOrDefault(_ => _.Id == sala.Id && _.Ativo);

                if (salaBD.IdJogadorSala.HasValue && salaBD.IdJogadorSala.Value > 0)
                    return salaBD;

                salaBD.AlterarJogador(sala.IdJogadorSala);
                this.Context.SaveChanges();
            }

            return sala;
        }

        public List<Sala> Listar()
        {
            return this.Context.Salas.ToList();
        }

        public Sala Obter(int idSala)
        {
            return this.Context.Salas.AsNoTracking().SingleOrDefault(_ => _.Id == idSala && _.Ativo);
        }
    }
}