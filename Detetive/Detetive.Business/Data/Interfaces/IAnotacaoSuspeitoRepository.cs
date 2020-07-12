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
        List<AnotacaoSuspeito> Adicionar(List<AnotacaoSuspeito> anotacoes);
        AnotacaoSuspeito Alterar(int idSuspeito, int idJogadorSala, bool valor);
    }
}