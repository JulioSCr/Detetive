using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IAnotacaoLocalBusiness
    {
        List<AnotacaoLocal> Listar();
        AnotacaoLocal Adicionar(int idLocal, int idJogadorSala);
        AnotacaoLocal Marcar(int idJogadorSala, int idLocal, bool valor);
    }
}
