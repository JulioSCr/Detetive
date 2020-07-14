using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Data.Interfaces
{
    public interface IJogadorRepository
    {
        Jogador Adicionar(Jogador jogador);
        Jogador Obter(int idJogador);
        List<Jogador> Listar(List<int> idJogadores);
    }
}
