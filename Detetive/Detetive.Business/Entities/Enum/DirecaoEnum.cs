using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities.Enum
{
    public enum DirecaoEnum
    {
        [Description("cima")]
        Cima = 1,
        [Description("baixo")]
        Baixo = 2,
        [Description("direita")]
        Direita = 3,
        [Description("esquerda")]
        Esquerda = 4
    }
}