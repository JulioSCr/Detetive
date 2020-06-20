using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class CrimeConfig : EntityTypeConfiguration<Crime>
    {
        public CrimeConfig()
        {
            ToTable("CRIME", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_CRIME");
            Property(p => p.IdSuspeito).HasColumnName("ID_SUSPEITO");
            Property(p => p.IdArma).HasColumnName("ID_ARMA");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.IdJogadorSala).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdSala).HasColumnName("ID_SALA");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}