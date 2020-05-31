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
        List<AnotacaoArma> Listar();
        AnotacaoArma Adicionar(int idArma, int idJogadorSala);
        AnotacaoArma Marcar(int idJogadorSala, int idArma, bool valor);
    }
}
