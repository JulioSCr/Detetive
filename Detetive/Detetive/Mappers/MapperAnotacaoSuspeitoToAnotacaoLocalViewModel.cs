using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel.Anotacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperAnotacaoSuspeitoToAnotacaoLocalViewModel : Profile
    {
        public MapperAnotacaoSuspeitoToAnotacaoLocalViewModel()
        {
            CreateMap<AnotacaoSuspeito, AnotacaoSuspeitoViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Suspeito.Id))
                .ForMember(viewModel => viewModel.Descricao, _ => _.MapFrom(model => model.Suspeito.Descricao))
                .ForMember(viewModel => viewModel.Selecionado, _ => _.MapFrom(model => model.Marcado));
        }

        public override string ProfileName
        {
            get { return "MapperAnotacaoSuspeitoToAnotacaoLocalViewModel"; }
        }
    }
}