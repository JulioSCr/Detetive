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
            return new Random().Next(1, QuantidadeLados + 1);
        }
    }
}