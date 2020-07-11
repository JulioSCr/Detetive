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
    public class SuspeitoJogadorSalaBusiness : ISuspeitoJogadorSalaBusiness
    {
        private readonly ICrimeBusiness _crimeBusiness;
        private readonly ISuspeitoBusiness _suspeitoBusiness;
        private readonly IJogadorSalaBusiness _jogadorSalaBusiness;
        private readonly ISuspeitoJogadorSalaRepository _suspeitoJogadorSalaRepository;

        public SuspeitoJogadorSalaBusiness(ICrimeBusiness crimeBusiness,
                                           IJogadorSalaBusiness jogadorSalaBusiness,
                                           ISuspeitoJogadorSalaRepository suspeitoJogadorSalaRepository,
                                           ISuspeitoBusiness suspeitoBusiness)
        {
            _crimeBusiness = crimeBusiness;
            _jogadorSalaBusiness = jogadorSalaBusiness;
            _suspeitoJogadorSalaRepository = suspeitoJogadorSalaRepository;
            _suspeitoBusiness = suspeitoBusiness;
        }

        public SuspeitoJogadorSala Adicionar(int idSuspeito, int idJogadorSala)
        {
            var jogadorSala = _jogadorSalaBusiness.Obter(idJogadorSala);
            if (jogadorSala == default)
                throw new InvalidOperationException("Jogador não encotrado.");

            var suspeitosJogadorSala = _suspeitoJogadorSalaRepository.Obter(idSuspeito, idJogadorSala);
            if (suspeitosJogadorSala != null)
                throw new InvalidOperationException("Este jogador já possui esta carta.");

            var crime = _crimeBusiness.Obter(jogadorSala.IdSala);
            if (crime != null && crime.IdSuspeito == idSuspeito)
                throw new InvalidOperationException("Esta carta faz parte do crime e não pode ser dada ao jogador.");

            return _suspeitoJogadorSalaRepository.Adicionar(new SuspeitoJogadorSala(idSuspeito, idJogadorSala));
        }

        public List<SuspeitoJogadorSala> Listar(int idJogadorSala)
        {
            var suspeitosJogadorSala = _suspeitoJogadorSalaRepository.Listar(idJogadorSala);
            if (suspeitosJogadorSala != null && suspeitosJogadorSala.Any())
            {
                var suspeitos = _suspeitoBusiness.Listar();
                suspeitosJogadorSala.ForEach(suspeitoJogadorSala => suspeitoJogadorSala.Suspeito = suspeitos.First(_ => _.Id == suspeitoJogadorSala.IdSuspeito));
            }

            return suspeitosJogadorSala;
        }
    }
}