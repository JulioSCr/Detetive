using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Local : BaseEntity
    {
        public string Descricao { get; set; }
        public int CoordenadaALinha { get; set; }
        public int CoordenadaAColuna { get; set; }
        public int CoordenadaBLinha { get; set; }
        public int CoordenadaBColuna { get; set; }
        public string UrlImagem { get; set; }

        public virtual List<PortaLocal> Portas { get; set; }

        internal Local()
        {
            Portas = new List<PortaLocal>();
        }

        public Local(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Portas = new List<PortaLocal>();
        }

        public bool DentroLocal(int coordenadaLinha, int coordenadaColuna)
        {
            bool entreLinhas = CoordenadaALinha < CoordenadaBLinha ? CoordenadaALinha <= coordenadaLinha && CoordenadaBLinha >= coordenadaLinha :
                                                                         CoordenadaBLinha <= coordenadaLinha && CoordenadaALinha >= coordenadaLinha;

            bool entreColunas = CoordenadaAColuna < CoordenadaBColuna ? CoordenadaAColuna <= coordenadaColuna && CoordenadaBColuna >= coordenadaColuna :
                                                                            CoordenadaBColuna <= coordenadaColuna && CoordenadaAColuna >= coordenadaColuna;

            return entreLinhas && entreColunas;
        }

        public bool PortaLocal(int coordenadaOrigemLinha, int coordenadaOrigemColuna, int coordenadaDestinoLinha, int coordenadaDestinoColuna)
        {
            var porta = Portas.FirstOrDefault(_ => _.CoordenadaLinha == coordenadaDestinoLinha && _.CoordenadaColuna == coordenadaDestinoColuna);
            if (porta == default)
                return false;

            return porta.ValidarMovimento(coordenadaOrigemLinha, coordenadaOrigemColuna);
        }
    }
}
