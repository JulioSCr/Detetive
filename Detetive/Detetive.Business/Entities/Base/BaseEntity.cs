using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Business.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Marcado { get; set; }
        public bool Ativo { get; set; }
        
        protected BaseEntity()
        {
            Ativo = true;
            Marcado = false;
        }

        public void DefinirAtivo(bool ativo)
        {
            Ativo = ativo;
        }

        public void Sinalar(bool valor)
        {
            Marcado = valor;
        }
    }
}