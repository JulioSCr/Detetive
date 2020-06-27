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

        public bool ValidarMovimento(int coordenadaLinha, int coordenadaColuna)
        {
            var direcao = EnumExtensions.GetValueFromDescription<DirecaoEnum>(this.Direcao);

            switch (direcao)
            {
                case DirecaoEnum.Cima:
                    return CoordenadaLinha == coordenadaLinha + 1 &&
                               CoordenadaColuna == coordenadaColuna;

                case DirecaoEnum.Baixo:
                    return CoordenadaLinha == coordenadaLinha - 1 &&
                               CoordenadaColuna == coordenadaColuna;

                case DirecaoEnum.Direita:
                    return CoordenadaLinha == coordenadaLinha &&
                               CoordenadaColuna == coordenadaColuna - 1;

                case DirecaoEnum.Esquerda:
                    return CoordenadaLinha == coordenadaLinha &&
                               CoordenadaColuna == coordenadaColuna + 1;

                default:
                    return false;
            }
        }
    }
}