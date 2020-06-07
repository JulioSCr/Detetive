using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel.Anotacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperArmaToArmaViewModel : Profile
    {
        public MapperArmaToArmaViewModel()
        {
            //CreateMap<Arma, ArmaViewModel>()
                //.ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.));
        }

        public override string ProfileName
        {
            get { return "MapperArmaToArmaViewModel"; }
        }
    }
}