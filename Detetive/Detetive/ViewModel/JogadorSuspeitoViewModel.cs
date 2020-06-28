using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public class JogadorSuspeitoViewModel
    {
        public string IdDescricao { get; set; }
        public int IdJogadorSala { get; set; }
        public int CoordenadaLinha { get; set; }
        public int CoordenadaColuna { get; set; }
        public int? IdSuspeito { get; set; }
        public int? IdLocal { get; set; }
        public string DescricaoSuspeito { get; set; }
    }
}