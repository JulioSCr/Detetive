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
    public class SuspeitoBusiness : ISuspeitoBusiness
    {
        private readonly ISuspeitoRepository _suspeitoRepository;
        
        public SuspeitoBusiness(ISuspeitoRepository suspeitoRepository)
        {
            _suspeitoRepository = suspeitoRepository;
        }

        public List<Suspeito> Listar()
        {
            return _suspeitoRepository.Listar();
        }

        public Suspeito Obter(int idSuspeito)
        {
            return _suspeitoRepository.Obter(idSuspeito);
        }
    }
}
