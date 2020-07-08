using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class SuspeitoJogadorSala : CartaBaseEntity
    {
        public int IdSuspeito { get; set; }
        public virtual Crime Crime { get; set; }
        public virtual JogadorSala JogadorSala { get; set; }
        public virtual Suspeito Suspeito { get; set; }
        internal SuspeitoJogadorSala() { }
        public SuspeitoJogadorSala(int idSuspeito, int idJogadorSala) : base(idJogadorSala)
        {
            IdSuspeito = idSuspeito;
        }
    }
}
