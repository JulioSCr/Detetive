using Detetive.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Entities
{
    public class Crime : BaseEntity
    {
        public int IdSuspeito { get; set; }
        public int IdArma { get; set; }
        public int IdLocal { get; set; }
        public int IdJogadorSala { get; set; }
        public int IdSala { get; set; }
        
        public Crime(int idSala, int idSuspeito, int idArma, int idLocal)
        {
            IdSala = idSala;
            IdSuspeito = idSuspeito;
            IdArma = idArma;
            IdLocal = idLocal;
        }

        public bool ValidarAcusacaoCrime(int idSuspeito, int idArma, int idLocal)
        {
            return IdArma == idArma &&
                    IdLocal == idLocal &&
                     IdSuspeito == idSuspeito;
        }

        public void Alterar(Crime crime)
        {
            IdSuspeito = crime.IdSuspeito;
            IdArma = crime.IdArma;
            IdLocal = crime.IdLocal;
            IdJogadorSala = crime.IdJogadorSala;
            IdSala = crime.IdSala;
        }

        public void AlterarJogadorSala(int idJogadorSala)
        {
            IdJogadorSala = idJogadorSala;
        }
    }
}