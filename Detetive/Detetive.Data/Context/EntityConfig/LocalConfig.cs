using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class LocalConfig : EntityTypeConfiguration<Local>
    {
        public LocalConfig()
        {
            ToTable("LOCAL", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_LOCAL");
            Property(p => p.Descricao).HasColumnName("DS_LOCAL");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}