using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository
{
    public class JogadorSalaRepository : BaseRepository, IJogadorSalaRepository
    {
        public JogadorSalaRepository() : base()
        {

        }

        public JogadorSala Obter(int idJogadorSala)
        {
            return this.Context.JogadoresSala.AsNoTracking().SingleOrDefault(_ => _.Id == idJogadorSala && _.Ativo);
        }

        public JogadorSala Alterar(JogadorSala jogador)
        {
            var jogadorSala = this.Context.JogadoresSala.SingleOrDefault(_ => _.Id == jogador.Id && _.Ativo);

            if (jogadorSala != default)
            {
                jogadorSala.Alterar(jogador);
                this.Context.SaveChanges();
            }

            return jogadorSala;
        }
    }
}