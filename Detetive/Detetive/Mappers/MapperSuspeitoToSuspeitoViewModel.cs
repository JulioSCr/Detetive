using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperSuspeitoToSuspeitoViewModel : Profile
    {
        public MapperSuspeitoToSuspeitoViewModel()
        {
            CreateMap<Suspeito, SuspeitoViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Id))
                .ForMember(viewModel => viewModel.Descricao, _ => _.MapFrom(model => model.Descricao))
                .ForMember(viewModel => viewModel.UrlImagem, _ => _.MapFrom(model => model.UrlImagem))
                .ForMember(viewModel => viewModel.UrlImagemToken, _ => _.MapFrom(model => model.UrlImagemToken));
        }

        public override string ProfileName
        {
            get { return "MapperSuspeitoToSuspeitoViewModel"; }
        }
    }
}