using Detetive.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Context.EntityConfig
{
    public class JogadorSalaConfig : EntityTypeConfiguration<JogadorSala>
    {
        public JogadorSalaConfig()
        {
            ToTable("JOGADOR_SALA", "DBO");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Id).HasColumnName("ID_JOGADOR_SALA");
            Property(p => p.IdJogador).HasColumnName("ID_JOGADOR");
            Property(p => p.IdSala).HasColumnName("ID_SALA");
            Property(p => p.NumeroOrdem).HasColumnName("NR_ORDER");
            Property(p => p.NumeroPassagemSecreta).HasColumnName("NR_PASSAGEM_SECRETA");
            Property(p => p.VezJogador).HasColumnName("IE_VEZ");
            Property(p => p.CoordenadaColuna).HasColumnName("NR_COLUNA");
            Property(p => p.CoordenadaLinha).HasColumnName("NR_LINHA");
            Property(p => p.IdLocal).HasColumnName("ID_LOCAL");
            Property(p => p.QuantidadeMovimento).HasColumnName("QT_MOVIMENTO");
            Property(p => p.IdSuspeito).HasColumnName("ID_SUSPEITO");
            Property(p => p.RolouDados).HasColumnName("IE_ROLARDADOS");
            Property(p => p.RealizouPalpite).HasColumnName("IE_REALIZARPALPITE");
            Property(p => p.Ativo).HasColumnName("IE_ATIVO");
            Ignore(p => p.Suspeito);
        }
    }
}