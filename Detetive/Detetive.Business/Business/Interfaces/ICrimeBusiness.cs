using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Business.Business.Interfaces
{
    public interface ICrimeBusiness
    {
        Crime Adicionar(Sala sala);
        Crime Obter(int idSala);
        Crime Alterar(Crime crime);
    }
}