using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Operacao
    {
        public bool Status { get; set; }
        public string Retorno { get; set; }

        public Operacao(string retorno, bool status = true)
        {
            Status = status;
            Retorno = retorno;
        }
    }
}