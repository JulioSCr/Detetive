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
        private readonly IArmaBusiness _armaBusiness;
        private readonly ILocalBusiness _localBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;

        public CrimeBusiness(ICrimeRepository crimeRepository, IArmaBusiness armaBusiness, ILocalBusiness localBusiness, ISuspeitoBusiness suspeitoBusiness)
        {
            _crimeRepository = crimeRepository;
            _armaBusiness = armaBusiness;
            _localBusiness = localBusiness;
            _suspeitoBusiness = suspeitoBusiness;
        }

        public Crime Adicionar(Sala sala)
        {
            var crime = this.Obter(sala.Id);
            if (crime != default)
                return crime;

            var armas = _armaBusiness.Listar();
            var locais = _localBusiness.Listar();
            var suspeitos = _suspeitoBusiness.Listar();

            Random sorteio = new Random();

            var armaCrime = armas[sorteio.Next(armas.Count)];
            var localCrime = locais[sorteio.Next(locais.Count)];
            var suspeitoCrime = suspeitos[sorteio.Next(suspeitos.Count)];

            return _crimeRepository.Adicionar(new Crime(sala.Id, suspeitoCrime.Id, armaCrime.Id, localCrime.Id));
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
