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

        public PosicaoViewModel(int linha, int coluna, int? idLocal)
        {
            Linha = linha;
            Coluna = coluna;
            IdLocal = idLocal;
        }
    }
}