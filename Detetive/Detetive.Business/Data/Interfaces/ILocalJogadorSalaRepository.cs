using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface ILocalJogadorSalaRepository
    {
        LocalJogadorSala Adicionar(LocalJogadorSala localJogadorSala);
        List<LocalJogadorSala> Listar(int idJogadorSala);
    }
}
