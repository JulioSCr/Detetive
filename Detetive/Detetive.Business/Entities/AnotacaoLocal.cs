using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class AnotacaoLocal : AnotacaoBaseEntity
    {
        public int IdLocal { get; set; }
        public Local Local { get; set; }

        internal AnotacaoLocal() { }

        public AnotacaoLocal(int idLocal, int idJogadorSala) : base(idJogadorSala)
        {
            IdLocal = idLocal;
        }
    }
}