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

        public SalaBusiness(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public Sala Criar(int idJogador)
        {
            // Desenvolvimento
            Sala sala = new Sala();

            sala = _salaRepository.Adicionar(sala);

            return null;
        }
    }
}