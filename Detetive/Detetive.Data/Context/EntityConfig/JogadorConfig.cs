using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Context.EntityConfig
{
    public class JogadorConfig : EntityTypeConfiguration<Jogador>
    {
        public JogadorConfig()
        {
            //ToTable("JOGADOR", "DBO");
            //HasKey(p => p.Id);
            //Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(p => p.Id).HasColumnName("ID_JOGADOR");
            //Property(p => p.Descricao).HasColumnName("DS_JOGADOR");
            ////Property(p => p.Suspeito).HasColumnName("ID_LOCAL");
        }
    }
}