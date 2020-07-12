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
    public class ArmaJogadorSalaBusiness : IArmaJogadorSalaBusiness
    {
        private readonly IArmaBusiness _armaBusiness;
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IArmaJogadorSalaRepository _armaJogadorSalaRepository;

        public ArmaJogadorSalaBusiness(ICrimeBusiness crimeBusiness,
                                        IJogadorSalaBusiness jogadorSalaBusiness,
                                        IArmaJogadorSalaRepository armaJogadorSalaRepository,
                                        IArmaBusiness armaBusiness)
        {
            _crimeBusiness = crimeBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _armaJogadorSalaRepository = armaJogadorSalaRepository;
            _armaBusiness = armaBusiness;
        }

        public ArmaJogadorSala Adicionar(int idArma, int idJogadorSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default)
                throw new InvalidOperationException("Jogador não encotrado.");

            var armasJogadorSala = _armaJogadorSalaRepository.Obter(idArma, idJogadorSala);
            if (armasJogadorSala != null)
                return armasJogadorSala;

            var crime = _crimeBusiness.Obter(jogadorSala.IdSala);
            if (crime != null && crime.IdArma == idArma)
                throw new InvalidOperationException("Esta carta faz parte do crime e não pode ser dada ao jogador.");

            return _armaJogadorSalaRepository.Adicionar(new ArmaJogadorSala(idArma, idJogadorSala));
        }

        public List<ArmaJogadorSala> Listar(int idJogadorSala)
        {
            var armasJogadorSala = _armaJogadorSalaRepository.Listar(idJogadorSala);
            if (armasJogadorSala != null && armasJogadorSala.Any())
            {
                var armas = _armaBusiness.Listar();
                armasJogadorSala.ForEach(armaJogadorSala => armaJogadorSala.Arma = armas.First(_ => _.Id == armaJogadorSala.IdArma));
            }

            return armasJogadorSala;
        }

        public ArmaJogadorSala Obter(int idArma, int idJogadorSala)
        {
            return _armaJogadorSalaRepository.Obter(idArma, idJogadorSala);
        }
    }
}