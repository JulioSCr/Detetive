using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IPortaLocalBusiness
    {
        PortaLocal Obter(int coordenadaLinha, int coordenadaColuna);
        List<PortaLocal> Listar();
        List<PortaLocal> Listar(int idLocal);
    }
}