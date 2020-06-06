using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
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

        internal Local()
        {

        }

        public Local(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public bool DentroLocal(int coordenadaLinha, int coordenadaColuna)
        {
            bool entreLinhas = CoordenadaALinha < CoordenadaBLinha ? CoordenadaALinha <= coordenadaLinha && CoordenadaBLinha >= coordenadaLinha :
                                                                         CoordenadaBLinha <= coordenadaLinha && CoordenadaALinha >= coordenadaLinha;

            bool entreColunas = CoordenadaAColuna < CoordenadaBColuna ? CoordenadaAColuna <= coordenadaColuna && CoordenadaBColuna >= coordenadaColuna :
                                                                            CoordenadaBColuna <= coordenadaColuna && CoordenadaAColuna >= coordenadaColuna;

            return entreLinhas && entreColunas;
        }

        public bool PortaLocal(int coordenadaLinha, int coordenadaColuna)
        {
            return true;
        }
    }
}
