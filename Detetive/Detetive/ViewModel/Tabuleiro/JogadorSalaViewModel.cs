using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel.Tabuleiro
{
    public class JogadorSalaViewModel
    {
        public int Id { get; set; }
        public PosicaoViewModel Posicao { get; set; }

        internal JogadorSalaViewModel() { Posicao = new PosicaoViewModel(); }

        public JogadorSalaViewModel(int id, PosicaoViewModel posicao)
        {
            Id = id;

            if (posicao != null)
                Posicao = posicao;
            else
                Posicao = new PosicaoViewModel();
        }
    }
}