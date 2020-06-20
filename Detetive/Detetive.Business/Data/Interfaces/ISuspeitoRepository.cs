using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Data.Interfaces
{
    public interface ISuspeitoRepository
    {
        Suspeito Obter(int idSuspeito);
        List<Suspeito> Listar();
    }
}
