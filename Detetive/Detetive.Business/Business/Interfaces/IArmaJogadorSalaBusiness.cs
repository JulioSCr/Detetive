using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface IArmaJogadorSalaBusiness
    {
        ArmaJogadorSala Adicionar(int idArma, int idJogadorSala);
        List<ArmaJogadorSala> Listar(int idJogadorSala);
        ArmaJogadorSala Obter(int idArma, int idJogadorSala);
        void DesabilitarArmasJogador(int idJogadorSala);
    }
}
