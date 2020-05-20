using Detetive.Business.Entities;
using Detetive.Data.Context.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Data.Context
{
    public class DetetiveContext : DbContext
    {
        public DbSet<Suspeito> Suspeitos { get; set; }

        public DetetiveContext(DbContextOptions<DetetiveContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Suspeito>(new SuspeitoConfig());
        }
    }
}
