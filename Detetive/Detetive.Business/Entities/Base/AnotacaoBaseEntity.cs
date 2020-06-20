using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities.Base
{
    public abstract class AnotacaoBaseEntity : BaseEntity
    {
        public int IdJogadorSala { get; set; }
        public bool Marcado { get; set; }

        protected AnotacaoBaseEntity() { }
        
        protected AnotacaoBaseEntity(int idJogadorSala) 
        {
            IdJogadorSala = idJogadorSala;
            Marcado = true;
        }

        public void Sinalar(bool valor)
        {
            Marcado = valor;
        }
    }
}
