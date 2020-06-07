using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface IAnotacaoLocalRepository
    {
        List<AnotacaoLocal> Listar(int idJogadorSala);
        AnotacaoLocal Adicionar(AnotacaoLocal anotacao);
        AnotacaoLocal Marcar(int idJogadorSala, int idLocal, bool valor);
    }
}
