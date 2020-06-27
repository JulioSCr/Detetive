using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel.Tabuleiro
{
    public class PartidaViewModel
    {
        public int Id { get; set; }
        public List<JogadorSalaViewModel> Jogadores { get; set; }
    }
}