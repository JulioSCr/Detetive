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
    public class LocalBusiness : ILocalBusiness
    {
        private readonly ILocalRepository _localRepository;

        public LocalBusiness(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        public List<Local> Listar()
        {
            return _localRepository.Listar();
        }

        public Local Obter(int idLocal)
        {
            return _localRepository.Obter(idLocal);
        }
    }
}