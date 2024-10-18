using Domain;
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Dokument
            modelBuilder.Entity<DokumentEntity>()
                    .HasOne(d => d.Komitent)
                    .WithMany()
                    .HasForeignKey(d => d.SifraKomitenta)
                    .HasPrincipalKey(k => k.SifraKomitenta);

            modelBuilder.Entity<DokumentEntity>()
                .Property(d => d.BrojDokumenta).IsRequired();

            modelBuilder.Entity<DokumentEntity>()
                .Property(d => d.SifraKomitenta).IsRequired();

            modelBuilder.Entity<DokumentEntity>()
                .HasIndex(d => d.BrojDokumenta)
                .IsUnique();

            //StavkaDokumenta

            modelBuilder.Entity<StavkaDokumentaEntity>()
                .HasOne(s => s.Roba)
                .WithMany()
                .HasForeignKey(s => s.SifraRobe)
                .HasPrincipalKey(r => r.SifraRobe);

            modelBuilder.Entity<StavkaDokumentaEntity>()
                .HasOne(s => s.Dokument)
                .WithMany()
                .HasForeignKey(s => s.BrojDokumenta)
                .HasPrincipalKey(d => d.BrojDokumenta);

            modelBuilder.Entity<StavkaDokumentaEntity>()
                .Property(s => s.BrojDokumenta).IsRequired();

            modelBuilder.Entity<StavkaDokumentaEntity>()
                .Property(s => s.SifraRobe).IsRequired();

            //Roba

            modelBuilder.Entity<RobaEntity>()
                .Property(r => r.Naziv).IsRequired();

            modelBuilder.Entity<RobaEntity>()
                .Property(r => r.SifraRobe).IsRequired();

            modelBuilder.Entity<RobaEntity>()
                .Property(r => r.JedinicaMere).IsRequired();

            modelBuilder.Entity<RobaEntity>()
                .HasIndex(r => r.SifraRobe)
                .IsUnique();

            //Komitent

            modelBuilder.Entity<KomitentEntity>(komitent =>
            {
                komitent.Property(k => k.SifraKomitenta).IsRequired();
                komitent.Property(k => k.Naziv).IsRequired();
                komitent.Property(k => k.Adresa).IsRequired();
                komitent.Property(k => k.Grad).IsRequired();
            });

            modelBuilder.Entity<KomitentEntity>()
                .HasIndex(k => k.SifraKomitenta)
                .IsUnique();
        }
    }
}
