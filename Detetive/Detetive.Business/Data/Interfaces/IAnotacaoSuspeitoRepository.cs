using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface IAnotacaoSuspeitoRepository
    {
        List<AnotacaoSuspeito> Listar(int idJogadorSala);
        AnotacaoSuspeito Adicionar(AnotacaoSuspeito anotacao);
        AnotacaoSuspeito Marcar(int idJogadorSala, int idSuspeito, bool valor);
    }
}
