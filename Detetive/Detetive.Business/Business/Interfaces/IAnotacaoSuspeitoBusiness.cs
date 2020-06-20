using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IAnotacaoSuspeitoBusiness
    {
        List<AnotacaoSuspeito> Listar(int idJogadorSala);
        void CriarAnotacoes(int idJogadorSala);
        AnotacaoSuspeito Alterar(int idSuspeito, int idJogadorSala, bool valor);
    }
}
