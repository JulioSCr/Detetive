using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class AnotacaoSuspeito : AnotacaoBaseEntity
    {
        public int IdSuspeito { get; set; }

        internal AnotacaoSuspeito() { }

        public AnotacaoSuspeito(int idSuspeito, int idJogadorSala) : base(idJogadorSala)
        {
            IdSuspeito = idSuspeito;
        }
    }
}