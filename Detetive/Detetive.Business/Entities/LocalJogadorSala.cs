using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class LocalJogadorSala : CartaBaseEntity
    {
        public int IdLocal { get; set; }
        public virtual Crime Crime { get; set; }
        public virtual JogadorSala JogadorSala { get; set; }
        public virtual Local Local { get; set; }
        internal LocalJogadorSala() { }
        public LocalJogadorSala(int idLocal, int idJogadorSala) : base(idJogadorSala)
        {
            IdLocal = idLocal;
        }
    }
}
