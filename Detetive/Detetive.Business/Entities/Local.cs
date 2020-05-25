using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Local : BaseEntity
    {
        public string Descricao { get; set; }

        internal Local()
        {

        }

        public Local(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
