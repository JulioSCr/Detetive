using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IPartidaBusiness
    {
        Operacao Iniciar(int idJogadorSala, int idSala);
        Operacao RolarDados(int idJogadorSala, int idSala);
        Operacao Acusar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma);
        Operacao Palpitar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma);
        Operacao MoverJogador(int idJogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna);
        Operacao Finalizar(int idJogadorSala);
    }
}