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

        internal Jogador()
        {

        }

        public Jogador(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
