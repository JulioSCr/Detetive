using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class ArmaJogadorSala : CartaBaseEntity
    {
        public int IdArma { get; set; }
        public virtual Crime Crime { get; set; }
        public virtual JogadorSala JogadorSala { get; set; }
        internal ArmaJogadorSala() { }
        public ArmaJogadorSala(int idArma, int idJogadorSala) : base(idJogadorSala)
        {
            IdArma = idArma;
        }
    }
}
