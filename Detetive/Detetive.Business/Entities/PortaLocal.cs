using Detetive.Business.Entities.Base;
using Detetive.Business.Entities.Enum;
using Detetive.Business.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class PortaLocal : BaseEntity
    {
        public int IdLocal { get; set; }
        public int CoordenadaLinha { get; set; }
        public int CoordenadaColuna { get; set; }
        public string Direcao { get; set; }

        internal PortaLocal() : base()
        {
        }

        public bool ValidarMovimento(int novaCoordenadaLinha, int novaCoordenadaColuna)
        {
            var direcao = EnumExtensions.GetValueFromDescription<DirecaoEnum>(this.Direcao);

            switch (direcao) 
            {
                case DirecaoEnum.Cima:
                    return CoordenadaLinha == novaCoordenadaLinha + 1 &&
                               CoordenadaColuna == novaCoordenadaColuna;

                case DirecaoEnum.Baixo:
                    return CoordenadaLinha == novaCoordenadaLinha - 1 &&
                               CoordenadaColuna == novaCoordenadaColuna;

                case DirecaoEnum.Direita:
                    return CoordenadaLinha == novaCoordenadaLinha &&
                               CoordenadaColuna == novaCoordenadaColuna - 1;

                case DirecaoEnum.Esquerda:
                    return CoordenadaLinha == novaCoordenadaLinha &&
                               CoordenadaColuna == novaCoordenadaColuna + 1;

                default:
                    return false;
            }
        }
    }
}