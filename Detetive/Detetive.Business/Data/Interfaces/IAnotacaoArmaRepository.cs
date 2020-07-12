using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Data.Interfaces
{
    public interface IAnotacaoArmaRepository
    {
        List<AnotacaoArma> Listar(int idJogadorSala);
        AnotacaoArma Adicionar(AnotacaoArma anotacao);
        List<AnotacaoArma> Adicionar(List<AnotacaoArma> anotacoes);
        AnotacaoArma Marcar(int idArma, int idJogadorSala, bool valor);
    }
}