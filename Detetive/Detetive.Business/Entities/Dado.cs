using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Dado
    {
        public int QuantidadeLados { get; private set; }

        public Dado(int quantidadeLados)
        {
            QuantidadeLados = quantidadeLados;
        }

        public int Rolar()
        {
            Random random = new Random();
            return random.Next(1, QuantidadeLados + 1) + random.Next(1, QuantidadeLados + 1);
        }
    }
}