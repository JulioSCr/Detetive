using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class AnotacaoArma : AnotacaoBaseEntity
    {
        public int IdArma { get; set; }

        internal AnotacaoArma() { }

        public AnotacaoArma(int idArma, int idJogadorSala) : base(idJogadorSala)
        {
            IdArma = idArma;
        }
    }
}