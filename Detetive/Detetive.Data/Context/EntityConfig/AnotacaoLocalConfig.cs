using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class AnotacaoLocalConfig : EntityTypeConfiguration<AnotacaoLocal>
    {
        public AnotacaoLocalConfig()
        {
            ToTable("ANOTACAO_LOCAL", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_ANOTACAO_LOCAL");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}