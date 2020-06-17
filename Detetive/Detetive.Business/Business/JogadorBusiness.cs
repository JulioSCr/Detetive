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
    public class JogadorBusiness : IJogadorBusiness
    {
        private readonly IJogadorRepository _jogadorRepository;

        public JogadorBusiness(IJogadorRepository jogadorRepository)
        {
            _jogadorRepository = jogadorRepository;
        }
        public Jogador Adicionar(string dsJogador)
        {
            return _jogadorRepository.Adicionar(new Jogador(dsJogador));
        }

        public Jogador Obter(int idJogador)
        {
            return _jogadorRepository.Obter(idJogador);
        }
    }
}
