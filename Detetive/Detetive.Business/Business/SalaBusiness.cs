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
    public class SalaBusiness : ISalaBusiness
    {
        private readonly ISalaRepository _salaRepository;
        private readonly IJogadorBusiness _jogadorBusiness;

        public SalaBusiness(ISalaRepository salaRepository, IJogadorBusiness jogadorBusiness)
        {
            _salaRepository = salaRepository;
            _jogadorBusiness = jogadorBusiness;
        }

        public Sala Adicionar(int idJogador)
        {
            var jogador = _jogadorBusiness.Obter(idJogador);

            if (jogador == default)
                return null;

            return _salaRepository.Adicionar(new Sala());
        }

        public Sala Adicionar()
        {
           return _salaRepository.Adicionar(new Sala());
        }

        public Sala Obter(int idSala)
        {
            return _salaRepository.Obter(idSala);
        }
    }
}