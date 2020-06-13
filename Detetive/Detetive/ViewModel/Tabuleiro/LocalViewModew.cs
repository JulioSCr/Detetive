using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel.Tabuleiro
{
    public class LocalViewModew
    {
        public int Id { get; set; }
        public int NR_LINHA1 { get; set; }
        public int NR_COLUNA1 { get; set; }
        public int NR_LINHA2 { get; set; }
        public int NR_COLUNA2 { get; set; }
        public bool Ativo { get; set; }
    }
}