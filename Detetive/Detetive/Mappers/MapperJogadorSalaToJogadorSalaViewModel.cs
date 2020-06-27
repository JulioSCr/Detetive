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
                .ForMember(viewModel => viewModel.MinhaVez, _ => _.MapFrom(model => model.VezJogador))
                .AfterMap((model, viewModel) => viewModel.Posicao.Linha = model.CoordenadaLinha)
                .AfterMap((model, viewModel) => viewModel.Posicao.Coluna = model.CoordenadaColuna)
                .AfterMap((model, viewModel) => viewModel.Posicao.IdLocal = model.IdLocal);
        }

        public override string ProfileName
        {
            get { return "MapperJogadorSalaToJogadorSalaViewModel"; }
        }
    }
}