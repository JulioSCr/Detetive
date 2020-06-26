﻿using Detetive.Business.Data.Interfaces;
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
    public class ArmaJogadorSalaRepository : BaseRepository, IArmaJogadorSalaRepository
    {
        public ArmaJogadorSalaRepository() : base()
        {
        }

        public ArmaJogadorSala Adicionar(ArmaJogadorSala armaJogadorSala)
        {
            if (armaJogadorSala != default)
            {
                this.Context.ArmaJogadorSala.Add(armaJogadorSala);
                this.Context.SaveChanges();
            }

            return armaJogadorSala;
        }

        public List<ArmaJogadorSala> Listar(int idJogadorSala)
        {
            return this.Context.ArmaJogadorSala.AsNoTracking().Where(_ => _.IdJogadorSala == idJogadorSala && _.Ativo).ToList();
        }

    }
}