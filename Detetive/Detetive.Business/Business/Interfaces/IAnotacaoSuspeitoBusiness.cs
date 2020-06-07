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
        AnotacaoSuspeito Adicionar(int idSuspeito, int idJogadorSala);
        AnotacaoSuspeito Marcar(int idJogadorSala, int idSuspeito, bool valor);
    }
}
