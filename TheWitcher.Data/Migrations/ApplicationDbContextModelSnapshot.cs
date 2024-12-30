﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheWitcher.Data.Data;

#nullable disable

namespace TheWitcher.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TheWitcher.Data.Models.Ekwipunek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostacId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostacId");

                    b.ToTable("Ekwipunki");
                });

            modelBuilder.Entity("TheWitcher.Data.Models.Postac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Poziom")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RasaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Umiejetnosci")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Postacie");
                });

            modelBuilder.Entity("TheWitcher.Data.Models.Rasa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rasy");
                });

            modelBuilder.Entity("TheWitcher.Data.Models.Ekwipunek", b =>
                {
                    b.HasOne("TheWitcher.Data.Models.Postac", null)
                        .WithMany("Ekwipunek")
                        .HasForeignKey("PostacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheWitcher.Data.Models.Postac", b =>
                {
                    b.Navigation("Ekwipunek");
                });
#pragma warning restore 612, 618
        }
    }
}
