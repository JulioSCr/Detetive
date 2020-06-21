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
                .ForMember(viewModel => viewModel.IdDescricao, _ => _.MapFrom(model => model.Descricao.TratarString()));
        }

        public override string ProfileName
        {
            get { return "MapperLocalToLocalViewModel"; }
        }
    }
}