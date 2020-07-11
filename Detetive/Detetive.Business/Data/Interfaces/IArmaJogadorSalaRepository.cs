using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface IArmaJogadorSalaRepository
    {
        ArmaJogadorSala Obter(int idArma, int idJogadorSala);
        ArmaJogadorSala Adicionar(ArmaJogadorSala armaJogadorSala);
        List<ArmaJogadorSala> Listar(int idJogadorSala);
    }
}
