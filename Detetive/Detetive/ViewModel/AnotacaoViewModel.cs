using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Detetive.ViewModel.Anotacao;

namespace Detetive.ViewModel
{
    public class AnotacaoViewModel
    {
        public List<ArmaViewModel> Armas { get; set; }
        public List<LocalViewModel> Locais { get; set; }
        public List<SuspeitoViewModel> Suspeitos { get; set; }

        public AnotacaoViewModel(List<ArmaViewModel> armas, List<LocalViewModel> locais, List<SuspeitoViewModel> suspeitos)
        {
            Armas = armas;
            Locais = locais;
            Suspeitos = suspeitos;
        }
    }
}