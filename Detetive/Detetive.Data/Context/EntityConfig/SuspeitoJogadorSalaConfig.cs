using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class SuspeitoJogadorSalaConfig : EntityTypeConfiguration<SuspeitoJogadorSala>
    {
        public SuspeitoJogadorSalaConfig()
        {
            ToTable("SUSPEITO_JOGADOR_SALA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_SUSPEITO_JOGADOR_SALA");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdSuspeito).HasColumnName("ID_SUSPEITO");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
            Ignore(p => p.Crime);
            Ignore(p => p.JogadorSala);
        }
    }
}
