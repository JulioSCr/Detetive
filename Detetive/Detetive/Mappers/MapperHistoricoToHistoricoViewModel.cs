using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using Detetive.ViewModel.Anotacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperHistoricoToHistoricoViewModel : Profile
    {
        public MapperHistoricoToHistoricoViewModel()
        {
            CreateMap<Historico, HistoricoViewModel>()
                .ForMember(viewModel => viewModel.Descricao, _ => _.MapFrom(model => model. Descricao));
        }

        public override string ProfileName
        {
            get { return "MapperHistoricoToHistoricoViewModel"; }
        }
    }
}