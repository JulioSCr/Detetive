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
    public class MapperJogadorSalaToJogadorSuspeitoViewModel : Profile
    {
        public MapperJogadorSalaToJogadorSuspeitoViewModel()
        {
            CreateMap<JogadorSala, JogadorSuspeitoViewModel>()
                .ForMember(viewModel => viewModel.IdJogadorSala, _ => _.MapFrom(model => model.Id))
                .ForMember(viewModel => viewModel.CoordenadaLinha, _ => _.MapFrom(model => model.CoordenadaLinha))
                .ForMember(viewModel => viewModel.CoordenadaColuna, _ => _.MapFrom(model => model.CoordenadaColuna))
                .ForMember(viewModel => viewModel.CoordenadaColuna, _ => _.MapFrom(model => model.CoordenadaColuna))
                .ForMember(viewModel => viewModel.IdSuspeito, _ => _.MapFrom(model => model.Suspeito.Id))
                .ForMember(viewModel => viewModel.DescricaoSuspeito, _ => _.MapFrom(model => model.Suspeito.Descricao))
                .ForMember(viewModel => viewModel.IdDescricao, _ => _.MapFrom(model => model.Suspeito.Descricao.TratarString()))
                .ForMember(viewModel => viewModel.NickJogador, _ => _.MapFrom(model => model.Jogador.Descricao))
                .ForMember(viewModel => viewModel.IdLocal, _ => _.MapFrom(model => model.IdLocal));
        }

        public override string ProfileName
        {
            get { return "MapperJogadorSalaToJogadorSuspeitoViewModel"; }
        }
    }
}