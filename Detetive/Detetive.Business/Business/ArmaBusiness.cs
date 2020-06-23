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
    public class ArmaBusiness : IArmaBusiness
    {
        private readonly IArmaRepository _armaRepository;

        public ArmaBusiness(IArmaRepository armaRepository)
        {
            _armaRepository = armaRepository;
        }

        public List<Arma> Listar()
        {
            return _armaRepository.Listar();
        }

        public Arma Obter(int idArma)
        {
            return _armaRepository.Obter(idArma);
        }
    }
}