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
    public class PortaLocalBusiness : IPortaLocalBusiness
    {
        private readonly IPortaLocalRepository _portaLocalRepository;

        public PortaLocalBusiness(IPortaLocalRepository portaLocalRepository)
        {
            _portaLocalRepository = portaLocalRepository;
        }
        
        public List<PortaLocal> Listar()
        {
            return _portaLocalRepository.Listar();
        }

        public List<PortaLocal> Listar(int idLocal)
        {
            return _portaLocalRepository.Listar(idLocal);
        }
    }
}