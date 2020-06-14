using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IMovimentacaoBusiness
    {
        bool MoverJogador(int ID_JOGADOR, int linhaMovimento, int colunaMovimento);
    }
}
