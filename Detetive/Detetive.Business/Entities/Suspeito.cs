using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Suspeito : BaseEntity
    {
        public string Descricao { get; set; }
        public int IdLocal { get; set; }
        public virtual Local Local { get; set; }


        internal Suspeito()
        {

        }

        public Suspeito(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
