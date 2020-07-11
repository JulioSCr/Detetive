using Detetive.Business.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Detetive.Data.Context.EntityConfig
{
    public class SuspeitoConfig : EntityTypeConfiguration<Suspeito>
    {
        public SuspeitoConfig()
        {
            ToTable("SUSPEITO", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_SUSPEITO");
            Property(p => p.Descricao).HasColumnName("DS_SUSPEITO");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.UrlImagem).HasColumnName("DS_CAMINHO_IMAGEM");
            Property(p => p.UrlImagemToken).HasColumnName("DS_CAMINHO_IMAGEM_TOKEN");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}