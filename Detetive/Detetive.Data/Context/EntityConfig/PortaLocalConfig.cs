using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class PortaLocalConfig : EntityTypeConfiguration<PortaLocal>
    {
        public PortaLocalConfig()
        {
            ToTable("LOCAL_PORTA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_LOCAL_PORTA");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.CoordenadaLinha).HasColumnName("NR_LINHA");
            Property(p => p.CoordenadaColuna).HasColumnName("NR_COLUNA");
            Property(p => p.Direcao).HasColumnName("DS_DIRECAO");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}