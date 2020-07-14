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
    public class JogadorRepository : BaseRepository, IJogadorRepository
    {
        public JogadorRepository() : base()
        {

        }

        public Jogador Adicionar(Jogador jogador)
        {
            this.Context.Jogadores.Add(jogador);
            this.Context.SaveChanges();

            return jogador;
        }

        public Jogador Obter(int idJogador)
        {
            return this.Context.Jogadores.AsNoTracking().SingleOrDefault(_ => _.Id == idJogador);
        }

        public List<Jogador> Listar(List<int> idJogadores)
        {
            if (idJogadores != null && idJogadores.Any())
                return this.Context.Jogadores.AsNoTracking().Where(_ => idJogadores.Any(x => _.Id == x)).ToList();

            return null;
        }
    }
}