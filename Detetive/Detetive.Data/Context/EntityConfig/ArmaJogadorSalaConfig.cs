using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class ArmaJogadorSalaConfig : EntityTypeConfiguration<ArmaJogadorSala>
    {
        public ArmaJogadorSalaConfig()
        {
            ToTable("ARMA_JOGADOR_SALA", "DBO");
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_ARMA_JOGADOR_SALA");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdArma).HasColumnName("ID_ARMA");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
            Ignore(p => p.Crime);
            Ignore(p => p.JogadorSala);
        }
    }
}
