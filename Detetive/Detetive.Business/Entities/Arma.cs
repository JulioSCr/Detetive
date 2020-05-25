using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Arma : BaseEntity
    {
        public string Descricao { get; set; }

        internal Arma()
        {

        }

        public Arma(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
