using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel.Anotacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperAnotacaoLocalToAnotacaoLocalViewModel : Profile
    {
        public MapperAnotacaoLocalToAnotacaoLocalViewModel()
        {
            CreateMap<AnotacaoLocal, AnotacaoLocalViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Local.Id))
                .ForMember(viewModel => viewModel.Descricao, _ => _.MapFrom(model => model.Local.Descricao))
                .ForMember(viewModel => viewModel.Selecionado, _ => _.MapFrom(model => model.Marcado));
        }

        public override string ProfileName
        {
            get { return "MapperAnotacaoLocalToAnotacaoLocalViewModel"; }
        }
    }
}