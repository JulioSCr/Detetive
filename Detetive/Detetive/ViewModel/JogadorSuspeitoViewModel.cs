using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public class JogadorSuspeitoViewModel
    {
        public int IdJogadorSala { get; set; }
        public int IdSuspeito { get; set; }
        public string DescricaoSuspeito { get; set; }
        public string IdDescricao { get; set; }
    }
}