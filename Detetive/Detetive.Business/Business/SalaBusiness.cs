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
    public class SalaBusiness : ISalaBusiness
    {
        private readonly ISalaRepository _salaRepository;
        private readonly IJogadorBusiness _jogadorBusiness;

        public SalaBusiness(ISalaRepository salaRepository, IJogadorBusiness jogadorBusiness)
        {
            _salaRepository = salaRepository;
            _jogadorBusiness = jogadorBusiness;
        }

        public Sala Adicionar()
        {
           return _salaRepository.Adicionar(new Sala());
        }

        public Sala Alterar(Sala sala)
        {
            return _salaRepository.Alterar(sala);
        }

        public Sala Obter(int idSala)
        {
            return _salaRepository.Obter(idSala);
        }
    }
}