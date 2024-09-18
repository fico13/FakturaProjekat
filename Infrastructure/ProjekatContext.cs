﻿using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ProjekatContext : DbContext
    {
        public DbSet<DokumentEntity> Dokumenti { get; set; }
        public DbSet<KomitentEntity> Komitenti { get; set; }
        public DbSet<RobaEntity> Roba { get; set; }
        public DbSet<StavkaDokumentaEntity> Stavke { get; set; }
        public ProjekatContext()
        {

        }

        public ProjekatContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Host='localhost';Port='5432';Database='Projekat';Username='postgres';Password='admin';Timeout='120';CommandTimeout='0';");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DokumentEntity>()
                    .HasOne(d => d.Komitent)
                    .WithMany()
                    .HasForeignKey(d => d.KomitentId);

            modelBuilder.Entity<StavkaDokumentaEntity>()
                .HasOne(s => s.Roba)
                .WithMany()
                .HasForeignKey(s => s.RobaId);
        }
    }
}
