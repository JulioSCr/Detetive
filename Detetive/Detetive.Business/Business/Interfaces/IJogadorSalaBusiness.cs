using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IJogadorSalaBusiness
    {
        JogadorSala Mover(JogadorSala jogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna);
        JogadorSala Obter(int idJogadorSala);
        List<JogadorSala> Listar(int idSala);
        Operacao Acusar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma);
    }
}
