using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities.Base
{
    public abstract class CartaBaseEntity : BaseEntity
    {
        public int IdJogadorSala { get; set; }

        public CartaBaseEntity() { }

        protected CartaBaseEntity(int idJogadorSala)
        {
            IdJogadorSala = idJogadorSala;
        }
    }
}
