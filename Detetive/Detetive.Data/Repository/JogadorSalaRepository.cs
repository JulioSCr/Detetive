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

        public JogadorSala Obter(int idJogador, int idSala)
        {
            return this.Context.JogadoresSala.AsNoTracking().SingleOrDefault(_ => _.IdJogador == idJogador && _.IdSala == idSala && _.Ativo);
        }

        public JogadorSala ObterPorSuspeito(int idSuspeito, int idSala)
        {
            return this.Context.JogadoresSala.AsNoTracking().SingleOrDefault(_ => _.IdSuspeito == idSuspeito &&
                                                                                                _.IdSala == idSala && _.Ativo);
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

        public List<JogadorSala> Listar(int idSala)
        {
            return this.Context.JogadoresSala.AsNoTracking().Where(_ => _.IdSala == idSala && _.Ativo).ToList();
        }

        public JogadorSala Adicionar(JogadorSala jogadorSala)
        {
            this.Context.JogadoresSala.Add(jogadorSala);
            this.Context.SaveChanges();

            return jogadorSala;
        }
    }
}