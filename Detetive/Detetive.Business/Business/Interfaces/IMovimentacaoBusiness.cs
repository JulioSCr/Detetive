using Detetive.Business.Entities;
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
        Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna);
    }
}
