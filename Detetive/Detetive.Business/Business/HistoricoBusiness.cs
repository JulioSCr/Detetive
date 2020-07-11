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
    public class HistoricoBusiness : IHistoricoBusiness
    {
        private readonly IHistoricoRepository _historicoRepository;

        public HistoricoBusiness(IHistoricoRepository historicoRepository)
        {
            _historicoRepository = historicoRepository;
        }

        public Historico Adicionar(Historico historico)
        {
            return _historicoRepository.Adicionar(historico);
        }

        public List<Historico> Listar(int idSala)
        {
            return _historicoRepository.Listar(idSala);
        }
    }
}