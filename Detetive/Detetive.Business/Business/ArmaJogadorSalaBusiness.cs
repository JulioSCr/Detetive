﻿using Detetive.Business.Business.Interfaces;
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
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly IArmaJogadorSalaRepository _armaJogadorSalaRepository;

        public ArmaJogadorSalaBusiness( ICrimeBusiness crimeBusiness, 
                                        IJogadorSalaBusiness jogadorSalaBusiness, 
                                        IArmaJogadorSalaRepository armaJogadorSalaRepository)
        {
            _crimeBusiness = crimeBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _armaJogadorSalaRepository = armaJogadorSalaRepository;
        }

        public ArmaJogadorSala Adicionar(int idArma, int idJogadorSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            var armasJogadorSala = _armaJogadorSalaRepository.Listar(idJogadorSala);

            if (armasJogadorSala != null && armasJogadorSala.Any(armaJogadorSala => armaJogadorSala.IdArma == idArma))
                throw new InvalidOperationException("Este jogador já possui esta carta.");

            var crime = _crimeBusiness.Obter(jogadorSala.IdSala);
            if (crime != null && crime.IdArma == idArma)
                throw new InvalidOperationException("Esta carta faz parte do crime e não pode ser dada ao jogador.");

            var jogadoresSala = _jogadorSalaBusiness.Listar(jogadorSala.IdSala).Where(x => x.Id != idJogadorSala);
            foreach (var outroJogadorSala in jogadoresSala)
            {
                var armasOutroJogadorSala = _armaJogadorSalaRepository.Listar(outroJogadorSala.Id);
                if (armasOutroJogadorSala != null && armasOutroJogadorSala.Any(armaOutroJogadorSala => armaOutroJogadorSala.IdArma == idArma))
                    throw new InvalidOperationException("Esta carta já faz parte do baralho de outro jogador.");
            }

            return _armaJogadorSalaRepository.Adicionar(new ArmaJogadorSala(idArma, idJogadorSala));
        }

        public List<ArmaJogadorSala> Listar(int idJogadorSala)
        {
            return _armaJogadorSalaRepository.Listar(idJogadorSala);
        }
    }
}