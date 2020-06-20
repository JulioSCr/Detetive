using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Data.Interfaces
{
    public interface IJogadorSalaRepository
    {
        JogadorSala Obter(int idJogadorSala);
        JogadorSala Obter(int idJogador, int idSala);
        JogadorSala ObterPorSuspeito(int idSuspeito, int idSala);
        JogadorSala Alterar(JogadorSala jogador);
        List<JogadorSala> Listar(int idSala);
        JogadorSala Adicionar(JogadorSala jogadorSala);
    }
}