using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Selecionado { get; set; }
    }
}