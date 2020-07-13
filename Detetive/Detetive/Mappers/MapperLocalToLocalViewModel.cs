using AutoMapper;
using Detetive.Business.Entities;
using Detetive.Extensions;
using Detetive.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperLocalToLocalViewModel : Profile
    {
        public MapperLocalToLocalViewModel()
        {
            CreateMap<Local, LocalViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Id))
                .ForMember(viewModel => viewModel.Descricao, _ => _.MapFrom(model => model.Descricao))
                .ForMember(viewModel => viewModel.CoordenadaALinha, _ => _.MapFrom(model => model.CoordenadaALinha))
                .ForMember(viewModel => viewModel.CoordenadaAColuna, _ => _.MapFrom(model => model.CoordenadaAColuna))
                .ForMember(viewModel => viewModel.CoordenadaBLinha, _ => _.MapFrom(model => model.CoordenadaBLinha))
                .ForMember(viewModel => viewModel.CoordenadaBColuna, _ => _.MapFrom(model => model.CoordenadaBColuna))
                .ForMember(viewModel => viewModel.PassagemSecreta, _ => _.MapFrom(model => model.IdLocalPassagemSecreta))
                .ForMember(viewModel => viewModel.Portas, _ => _.MapFrom(model => model.Portas))
                .ForMember(viewModel => viewModel.IdDescricao, _ => _.MapFrom(model => model.Descricao.TratarString())); 
        }

        public override string ProfileName
        {
            get { return "MapperLocalToLocalViewModel"; }
        }
    }
}