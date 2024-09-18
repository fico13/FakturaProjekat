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

                    b.Property<DateTime>("DatumIzdavanja")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("KomitentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UkupnaCena")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("KomitentId");

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

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

                    b.HasKey("Id");

                    b.ToTable("Roba");
                });

            modelBuilder.Entity("Domain.StavkaDokumentaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CenaStavkeKom")
                        .HasColumnType("numeric");

                    b.Property<int>("DokumentId")
                        .HasColumnType("integer");

                    b.Property<int>("Kolicina")
                        .HasColumnType("integer");

                    b.Property<int>("RobaId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UkupnaCena")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("DokumentId");

                    b.HasIndex("RobaId");

                    b.ToTable("Stavke");
                });

            modelBuilder.Entity("Domain.DokumentEntity", b =>
                {
                    b.HasOne("Domain.KomitentEntity", "Komitent")
                        .WithMany()
                        .HasForeignKey("KomitentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komitent");
                });

            modelBuilder.Entity("Domain.StavkaDokumentaEntity", b =>
                {
                    b.HasOne("Domain.DokumentEntity", "Dokument")
                        .WithMany("Stavke")
                        .HasForeignKey("DokumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.RobaEntity", "Roba")
                        .WithMany()
                        .HasForeignKey("RobaId")
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
