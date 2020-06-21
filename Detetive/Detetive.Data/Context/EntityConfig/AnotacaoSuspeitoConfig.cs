using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class AnotacaoSuspeitoConfig : EntityTypeConfiguration<AnotacaoSuspeito>
    {
        public AnotacaoSuspeitoConfig()
        {
            ToTable("ANOTACAO_SUSPEITO", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_ANOTACAO_SUSPEITO");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdSuspeito).HasColumnName("ID_SUSPEITO");
            Property(p => p.Marcado).HasColumnName("IE_ANOTADO");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
            Ignore(p => p.Suspeito);
        }
    }
}