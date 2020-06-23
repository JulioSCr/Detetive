using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.ViewModel
{
    public class SuspeitoViewModel
    {
        public int Id { get; set; }
        public int? IdJogadorSala { get; set; }
        public string Descricao { get; set; }
        public string UrlImagem { get; set; }
        public string NickJogador { get; set; }
    }
}