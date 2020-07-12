using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Mappers
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperLocalToLocalViewModel>();
                x.AddProfile<MapperSuspeitoToSuspeitoViewModel>();
                x.AddProfile<MapperJogadorSalaToJogadorSuspeitoViewModel>();
                x.AddProfile<MapperAnotacaoArmaToAnotacaoArmaViewModel>();
                x.AddProfile<MapperAnotacaoLocalToAnotacaoLocalViewModel>();
                x.AddProfile<MapperAnotacaoSuspeitoToAnotacaoLocalViewModel>();
                x.AddProfile<MapperJogadorSalaToJogadorSalaViewModel>();
                x.AddProfile<MapperHistoricoToHistoricoViewModel>();
                x.AddProfile<MapperArmaToArmaViewModel>();
                x.AddProfile<MapperSalaToSalaViewModel>();
            });
        }
    }
}