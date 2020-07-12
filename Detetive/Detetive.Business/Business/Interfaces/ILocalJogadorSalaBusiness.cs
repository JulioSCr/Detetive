using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface ILocalJogadorSalaBusiness
    {
        LocalJogadorSala Adicionar(int idLocal, int idJogadorSala);
        List<LocalJogadorSala> Listar(int idJogadorSala);
        LocalJogadorSala Obter(int idLocal, int idJogadorSala);
        void DesabilitarLocaisJogador(int idJogadorSala);
    }
}