using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel.Tabuleiro
{
    public class PosicaoViewModel
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public int? IdLocal { get; set; }

        internal PosicaoViewModel() { Linha = 1; Coluna = 1; }
    }
}