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
        private readonly IPortaLocalBusiness _portaLocalBusiness;

        public LocalBusiness(ILocalRepository localRepository,
                             IPortaLocalBusiness portaLocalBusiness)
        {
            _localRepository = localRepository;
            _portaLocalBusiness = portaLocalBusiness;
        }

        public List<Local> Listar()
        {
            var locais = _localRepository.Listar();
            var portasLocais = _portaLocalBusiness.Listar();

            locais.ForEach(local => local.Portas = portasLocais.Where(p => p.IdLocal == local.Id).ToList());

            return locais;
        }

        public Local Obter(int idLocal)
        {
            return _localRepository.Obter(idLocal);
        }

        public Local Obter(int coordenadaLinha, int coordenadaColuna)
        {
            var local = _localRepository.Obter(coordenadaLinha, coordenadaColuna);

            if (local != default)
                local.Portas = _portaLocalBusiness.Listar(local.Id);

            return local;
        }
    }
}