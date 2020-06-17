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
        Operacao Adicionar(Sala sala, int idJogador);
        JogadorSala Obter(int idJogadorSala);
        JogadorSala Obter(int idJogador, int idSala);
        List<JogadorSala> Listar(int idSala);
        JogadorSala Mover(JogadorSala jogadorSala, int novaCoordenadaLinha, int novaCoordenadaColuna);
        Operacao Acusar(int idSala, int idJogadorSala, int idLocal, int idSuspeito, int idArma);
    }
}
