﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ProjekatContext))]
    partial class ProjekatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.DokumentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BrojDokumenta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DatumDospeca")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DatumIzdavanja")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SifraKomitenta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UkupnaCena")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BrojDokumenta")
                        .IsUnique();

                    b.HasIndex("SifraKomitenta");

                    b.ToTable("Dokumenti");
                });

            modelBuilder.Entity("Domain.KomitentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SifraKomitenta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SifraKomitenta")
                        .IsUnique();

                    b.ToTable("Komitenti");
                });

            modelBuilder.Entity("Domain.RobaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cena")
                        .HasColumnType("numeric");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SifraRobe")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SifraRobe")
                        .IsUnique();

                    b.ToTable("Roba");
                });

            modelBuilder.Entity("Domain.StavkaDokumentaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BrojDokumenta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("DokumentEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("Kolicina")
                        .HasColumnType("integer");

                    b.Property<string>("SifraRobe")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UkupnaCenaStavke")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BrojDokumenta");

                    b.HasIndex("DokumentEntityId");

                    b.HasIndex("SifraRobe");

                    b.ToTable("Stavke");
                });

            modelBuilder.Entity("Domain.DokumentEntity", b =>
                {
                    b.HasOne("Domain.KomitentEntity", "Komitent")
                        .WithMany()
                        .HasForeignKey("SifraKomitenta")
                        .HasPrincipalKey("SifraKomitenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komitent");
                });

            modelBuilder.Entity("Domain.StavkaDokumentaEntity", b =>
                {
                    b.HasOne("Domain.DokumentEntity", "Dokument")
                        .WithMany()
                        .HasForeignKey("BrojDokumenta")
                        .HasPrincipalKey("BrojDokumenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.DokumentEntity", null)
                        .WithMany("Stavke")
                        .HasForeignKey("DokumentEntityId");

                    b.HasOne("Domain.RobaEntity", "Roba")
                        .WithMany()
                        .HasForeignKey("SifraRobe")
                        .HasPrincipalKey("SifraRobe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dokument");

                    b.Navigation("Roba");
                });

            modelBuilder.Entity("Domain.DokumentEntity", b =>
                {
                    b.Navigation("Stavke");
                });
#pragma warning restore 612, 618
        }
    }
}
