using AutoMapper;
using Detetive.Business.Entities;
using Detetive.ViewModel.Tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public class MapperJogadorSalaToJogadorSalaViewModel : Profile
    {
        public MapperJogadorSalaToJogadorSalaViewModel()
        {
            CreateMap<JogadorSala, JogadorSalaViewModel>()
                .ForMember(viewModel => viewModel.Id, _ => _.MapFrom(model => model.Id))
                .ForMember(viewModel => viewModel.Posicao.Linha, _ => _.MapFrom(model => model.CoordenadaLinha))
                .ForMember(viewModel => viewModel.Posicao.Coluna, _ => _.MapFrom(model => model.CoordenadaColuna));
        }

        public override string ProfileName
        {
            get { return "MapperJogadorSalaToJogadorSalaViewModel"; }
        }
    }
}