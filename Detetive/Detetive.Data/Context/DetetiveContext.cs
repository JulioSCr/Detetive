using Detetive.Business.Entities;
using Detetive.Data.Context.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Detetive.Data.Context
{
    public class DetetiveContext : DbContext
    {
        // Entidades Fortes
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Suspeito> Suspeitos { get; set; }

        // Entidades Fracas - Anotações Jogador
        public DbSet<AnotacaoArma> AnotacaoArmas { get; set; }
        public DbSet<AnotacaoLocal> AnotacaoLocais { get; set; }
        public DbSet<AnotacaoSuspeito> AnotacaoSuspeitos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArmaConfig());
            modelBuilder.Configurations.Add(new LocalConfig());
            modelBuilder.Configurations.Add(new SalaConfig());
            modelBuilder.Configurations.Add(new SuspeitoConfig());
            modelBuilder.Configurations.Add(new AnotacaoArmaConfig());
            modelBuilder.Configurations.Add(new AnotacaoLocalConfig());
            modelBuilder.Configurations.Add(new AnotacaoSuspeitoConfig());

            Database.SetInitializer<DetetiveContext>(null);
        }
    }
}