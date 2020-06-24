using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class LocalJogadorSalaConfig : EntityTypeConfiguration<LocalJogadorSala>
    {
        public LocalJogadorSalaConfig()
        {
            ToTable("LOCAL_JOGADOR_SALA", "DBO");
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_LOCAL_JOGADOR_SALA");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
            Ignore(p => p.Crime);
            Ignore(p => p.JogadorSala);
        }
    }
}
