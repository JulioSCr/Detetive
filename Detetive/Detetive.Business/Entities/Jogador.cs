using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? IdSuspeito { get; set; }
        //public Suspeito Suspeito { get; set; }

        internal Jogador()
        {

        }

        public Jogador(int id, string descricao, int idSuspeito /*, Suspeito suspeito*/)
        {
            Id = id;
            Descricao = descricao;
            IdSuspeito = idSuspeito;
            //Suspeito = suspeito;
        }
    }
}
