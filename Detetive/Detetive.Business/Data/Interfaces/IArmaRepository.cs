using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface IArmaRepository
    {
        Arma Obter(int idArma);
        List<Arma> Listar();
    }
}
