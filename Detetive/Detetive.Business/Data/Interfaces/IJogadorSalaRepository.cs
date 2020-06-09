using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Data.Interfaces
{
    public interface IJogadorSalaRepository
    {
        JogadorSala Obter(int idJogadorSala);
        JogadorSala Alterar(JogadorSala jogador);
        List<JogadorSala> Listar(int idSala);
    }
}