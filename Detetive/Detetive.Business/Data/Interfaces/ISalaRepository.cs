using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface ISalaRepository
    {
        Sala Adicionar(Sala sala);
        List<Sala> Listar();
        Sala Obter(int idSala);
        Sala Alterar(Sala sala);
    }
}
