using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business
{
    public class JogadorSalaBusiness : IJogadorSalaBusiness
    {
        private readonly IJogadorSalaRepository _jogadorSalaRepository;

        public JogadorSalaBusiness(IJogadorSalaRepository jogadorSalaRepository)
        {
            _jogadorSalaRepository = jogadorSalaRepository;
        }

        public JogadorSala Mover(JogadorSala jogador, int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            jogador.Mover(novaCoordenadaLinha, novaCoordenadaColuna);

            return _jogadorSalaRepository.Alterar(jogador);
        }

        public JogadorSala Obter(int idJogadorSala)
        {
            return _jogadorSalaRepository.Obter(idJogadorSala);
        }
    }
}