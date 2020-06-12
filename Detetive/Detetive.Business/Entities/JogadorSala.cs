using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class JogadorSala
    {
        public int Id { get; set; }
        public Sala Sala { get; set; }
        public int NumeroOrdem { get; set; }
        public int NumeroPassagemSecreta { get; set; }
        public bool VezJogador { get; set; }
        public int QuantidadeMovimento { get; set; }
        public bool Ativo { get; set; }
        public Jogador Jogador { get; set; }

        internal JogadorSala()
        {

        }
    }
}