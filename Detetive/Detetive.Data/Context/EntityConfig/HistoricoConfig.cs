using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class HistoricoConfig : EntityTypeConfiguration<Historico>
    {
        public HistoricoConfig()
        {
            ToTable("HISTORICO", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_HISTORICO");
            Property(p => p.IdSala).HasColumnName("ID_SALA");
            Property(p => p.Descricao).HasColumnName("DS_EVENTO");
            Property(p => p.DataCriacao).HasColumnName("DT_EVENTO");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}