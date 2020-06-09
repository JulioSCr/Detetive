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
        private readonly ISuspeitoRepository _suspeitoRepository;
        private readonly IJogadorSalaRepository _jogadorSalaRepository;

        public JogadorSalaBusiness(ISuspeitoRepository suspeitoRepository,
                                   IJogadorSalaRepository jogadorSalaRepository)
        {
            _suspeitoRepository = suspeitoRepository;
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

        public List<JogadorSala> Listar(int idSala)
        {
            var listaJogadores = _jogadorSalaRepository.Listar(idSala);
            var suspeitos = _suspeitoRepository.Listar();

            listaJogadores.ForEach(x => x.Suspeito = suspeitos.FirstOrDefault(y => y.Id == x.IdSuspeito));

            return listaJogadores;
        }
    }
}