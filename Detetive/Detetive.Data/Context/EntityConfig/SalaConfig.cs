using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class SalaConfig : EntityTypeConfiguration<Sala>
    {
        public SalaConfig()
        {
            ToTable("SALA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_SALA");
            Property(p => p.DataCriacao).HasColumnName("DT_CRIACAO");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}