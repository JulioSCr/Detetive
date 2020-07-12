using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperSalaToSalaViewModel : Profile
    {
        public MapperSalaToSalaViewModel()
        {
            CreateMap<Sala, SalaViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Id))
                .ForMember(viewModel => viewModel.DataCriacao, _ => _.MapFrom(model => model.DataCriacao))
                .ForMember(viewModel => viewModel.Ativo, _ => _.MapFrom(model => model.Ativo))
                .ForMember(viewModel => viewModel.IdJogadorSala, _ => _.MapFrom(model => model.IdJogadorSala));
        }

        public override string ProfileName
        {
            get { return "MapperSalaToSalaViewModel"; }
        }
    }
}