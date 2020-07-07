using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Historico : BaseEntity
    {
        public int IdSala { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        internal Historico() { }

        public Historico(int idSala, string descricao) : base()
        {
            IdSala = idSala;
            Descricao = descricao;
            DataCriacao = DateTime.Now;
        }
    }
}