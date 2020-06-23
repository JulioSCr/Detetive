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
    public class CrimeBusiness : ICrimeBusiness
    {
        private readonly ICrimeRepository _crimeRepository;

        public CrimeBusiness(ICrimeRepository crimeRepository)
        {
            _crimeRepository = crimeRepository;
        }

        public Crime Alterar(Crime crime)
        {
            return _crimeRepository.Alterar(crime);
        }

        public Crime Obter(int idSala)
        {
            return _crimeRepository.Obter(idSala);
        }
    }
}
