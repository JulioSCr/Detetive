using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Sala : BaseEntity
    {
        public int? IdJogadorSala { get; set; }
        public DateTime DataCriacao { get; set; }

        internal Sala() : base()
        {
            DataCriacao = DateTime.Now;
        }

        public Sala(int id)
        {
            Id = id;
            DataCriacao = DateTime.Now;
        }

        public void AlterarJogador(int? idJogadorSala)
        {
            IdJogadorSala = idJogadorSala;
        }
    }
}