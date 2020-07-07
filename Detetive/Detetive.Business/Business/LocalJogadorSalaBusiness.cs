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
    public class LocalJogadorSalaBusiness : ILocalJogadorSalaBusiness
    {
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly ILocalJogadorSalaRepository _localJogadorSalaRepository;

        public LocalJogadorSalaBusiness(ICrimeBusiness crimeBusiness, 
                                        IJogadorSalaBusiness jogadorSalaBusiness, 
                                        ILocalJogadorSalaRepository localJogadorSalaRepository)
        {
            _crimeBusiness = crimeBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _localJogadorSalaRepository = localJogadorSalaRepository;
        }

        public LocalJogadorSala Adicionar(int idLocal, int idJogadorSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default)
                throw new InvalidOperationException("Jogador não encotrado.");

            var locaisJogadorSala = _localJogadorSalaRepository.Obter(idLocal, idJogadorSala);
            if (locaisJogadorSala != null)
                return locaisJogadorSala;

            var crime = _crimeBusiness.Obter(jogadorSala.IdSala);
            if (crime != null && crime.IdLocal == idLocal)
                throw new InvalidOperationException("Esta carta faz parte do crime e não pode ser dada ao jogador.");

            var jogadoresSala = _jogadorSalaBusiness.Listar(jogadorSala.IdSala).Where(x => x.Id != idJogadorSala);
            foreach (var outroJogadorSala in jogadoresSala)
            {
                var locaisOutroJogadorSala = _localJogadorSalaRepository.Listar(outroJogadorSala.Id);
                if (locaisOutroJogadorSala != null && locaisOutroJogadorSala.Any(localOutroJogadorSala => localOutroJogadorSala.IdLocal == idLocal))
                    throw new InvalidOperationException("Esta carta já faz parte do baralho de outro jogador.");
            }

            return _localJogadorSalaRepository.Adicionar(new LocalJogadorSala(idLocal, idJogadorSala));
        }

        public List<LocalJogadorSala> Listar(int idJogadorSala)
        {
            return _localJogadorSalaRepository.Listar(idJogadorSala);
        }
    }
}
