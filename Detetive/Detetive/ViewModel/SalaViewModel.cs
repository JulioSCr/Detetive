using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public class SalaViewModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public int IdJogadorSala { get; set; }
    }
}