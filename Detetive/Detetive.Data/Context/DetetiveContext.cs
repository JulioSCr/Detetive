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
        public DbSet<Suspeito> Suspeitos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SuspeitoConfig());

            Database.SetInitializer<DetetiveContext>(null);
        }
    }
}