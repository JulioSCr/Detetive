using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class AnotacaoArmaConfig : EntityTypeConfiguration<AnotacaoArma>
    {
        public AnotacaoArmaConfig()
        {
            ToTable("ANOTACAO_ARMA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_ANOTACAO_ARMA");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdArma).HasColumnName("ID_ARMA");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}