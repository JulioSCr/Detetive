using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class ArmaConfig : EntityTypeConfiguration<Arma>
    {
        public ArmaConfig()
        {
            ToTable("ARMA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_ARMA");
            Property(p => p.Descricao).HasColumnName("DS_ARMA");
            Property(p => p.UrlImagem).HasColumnName("DS_CAMINHO_IMAGEM");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}