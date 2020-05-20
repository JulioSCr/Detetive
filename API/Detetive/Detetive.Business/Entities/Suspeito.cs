using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities
{
    public class Suspeito : BaseEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

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
