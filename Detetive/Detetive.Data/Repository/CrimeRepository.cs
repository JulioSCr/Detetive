using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Detetive.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository
{
    public class CrimeRepository : BaseRepository, ICrimeRepository
    {
        public CrimeRepository() : base()
        {

        }

        public Crime Obter(int idSala)
        {
            return this.Context.Crimes.AsNoTracking().FirstOrDefault(_ => _.IdSala == idSala && _.Ativo);
        }

        public Crime Alterar(Crime crime)
        {
            var crimeSala = this.Context.Crimes.SingleOrDefault(_ => _.Id == crime.Id && _.Ativo);

            if (crimeSala != default)
            {
                crimeSala.Alterar(crime);
                this.Context.SaveChanges();
            }

            return crimeSala;
        }

        public Crime Adicionar(Crime crime)
        {
            this.Context.Crimes.Add(crime);
            this.Context.SaveChanges();

            return crime;
        }
    }
}