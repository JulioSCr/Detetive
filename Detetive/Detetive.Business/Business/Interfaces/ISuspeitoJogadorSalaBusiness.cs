using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface ISuspeitoJogadorSalaBusiness
    {
        SuspeitoJogadorSala Adicionar(int idSuspeito, int idJogadorSala);
        List<SuspeitoJogadorSala> Listar(int idJogadorSala);
        SuspeitoJogadorSala Obter(int idSuspeito, int idJogadorSala);
    }
}