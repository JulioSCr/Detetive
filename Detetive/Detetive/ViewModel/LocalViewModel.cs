using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public class LocalViewModel : BaseViewModel
    {
        public string IdDescricao { get; set; }
        public int CoordenadaALinha { get; set; }
        public int CoordenadaAColuna { get; set; }
        public int CoordenadaBLinha { get; set; }
        public int CoordenadaBColuna { get; set; }
        public List<PortaLocal> Portas { get; set; }
    }
}