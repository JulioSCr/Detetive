using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        
        protected BaseEntity()
        {
            Ativo = true;
        }

        public void DefinirAtivo(bool ativo)
        {
            Ativo = ativo;
        }
    }
}