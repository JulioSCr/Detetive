using Detetive.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Data.Context.EntityConfig
{
    public class SuspeitoConfig : IEntityTypeConfiguration<Suspeito>
    {
        public void Configure(EntityTypeBuilder<Suspeito> builder)
        {
            builder.ToTable("SUSPEITO");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID_SUSPEITO");
            builder.Property(x => x.Descricao).HasColumnName("DS_SUSPEITO");
            builder.Property(x => x.Ativo).HasColumnName("IE_ATIVO");
        }
    }
}