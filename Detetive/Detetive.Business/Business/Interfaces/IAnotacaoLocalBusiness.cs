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
        List<AnotacaoLocal> Listar(int idJogadorSala);
        void CriarAnotacoes(int idJogadorSala);
        AnotacaoLocal Alterar(int idLocal, int idJogadorSala, bool valor);
    }
}
