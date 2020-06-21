using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class PortaLocal : BaseEntity
    {
        public int IdLocal { get; set; }
        public int CoordenadaLinha { get; set; }
        public int CoordenadaColuna { get; set; }
        public string Direcao { get; set; }

        internal PortaLocal() : base()
        {
        }
    }
}