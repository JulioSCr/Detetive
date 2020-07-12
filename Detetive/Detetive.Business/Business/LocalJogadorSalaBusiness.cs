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
        private readonly ILocalBusiness _localBusiness;

        public LocalJogadorSalaBusiness(ICrimeBusiness crimeBusiness,
                                        IJogadorSalaBusiness jogadorSalaBusiness,
                                        ILocalJogadorSalaRepository localJogadorSalaRepository,
                                        ILocalBusiness localBusiness)
        {
            _crimeBusiness = crimeBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _localJogadorSalaRepository = localJogadorSalaRepository;
            _localBusiness = localBusiness;
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

            return _localJogadorSalaRepository.Adicionar(new LocalJogadorSala(idLocal, idJogadorSala));
        }

        public void DesabilitarLocaisJogador(int idJogadorSala)
        {
            var locais = _localJogadorSalaRepository.Listar(idJogadorSala);

            locais.ForEach(local => local.Ativo = false);

            _localJogadorSalaRepository.Alterar(locais);
        }

        public List<LocalJogadorSala> Listar(int idJogadorSala)
        {
            var locaisJogadorSala = _localJogadorSalaRepository.Listar(idJogadorSala);
            if (locaisJogadorSala != null && locaisJogadorSala.Any())
            {
                var locais = _localBusiness.Listar();
                locaisJogadorSala.ForEach(localJogadorSala => localJogadorSala.Local = locais.First(_ => _.Id == localJogadorSala.IdLocal));
            }

            return locaisJogadorSala;
        }

        public LocalJogadorSala Obter(int idLocal, int idJogadorSala)
        {
            return _localJogadorSalaRepository.Obter(idLocal, idJogadorSala);
        }
    }
}