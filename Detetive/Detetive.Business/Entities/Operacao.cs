using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Operacao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public Operacao(string mensagem, bool sucesso = true)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}