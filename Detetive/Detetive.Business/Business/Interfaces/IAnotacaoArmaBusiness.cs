using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IAnotacaoArmaBusiness
    {
        List<AnotacaoArma> Listar(int idJogadorSala);
        void CriarAnotacoes(int idJogadorSala);
        AnotacaoArma Alterar(int idArma, int idJogadorSala, bool valor);
    }
}
