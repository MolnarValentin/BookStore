﻿// <auto-generated />
using System;
using KönyvtárAlkalmazás.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221114184232_fixKonyvFinal")]
    partial class fixKonyvFinal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Előkölcsönzés", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AzonosítóKód")
                        .HasColumnType("int");

                    b.Property<int?>("FelhasználóId")
                        .HasColumnType("int");

                    b.Property<DateTime>("KezdetiDátum")
                        .HasColumnType("datetime2");

                    b.Property<string>("KölcsönzőEmailCíme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KölcsönzőTelefonszáma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KönyvId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LejáratiDátum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FelhasználóId");

                    b.HasIndex("KönyvId");

                    b.ToTable("Előkölcsönzések");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Felhasználó", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Felhasználónév")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jelszó")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keresztnév")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefonszám")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vezetéknév")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Felhasználók");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Kölcsönzés", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FelhasználóId")
                        .HasColumnType("int");

                    b.Property<string>("KölcsönzőEmailCíme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KölcsönzőTelefonszáma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KönyvId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LejáratiDátum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FelhasználóId");

                    b.HasIndex("KönyvId");

                    b.ToTable("Kölcsönzések");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Könyv", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cím")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Előkölcsönözték")
                        .HasColumnType("bit");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kiadó")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Kikölcsönözték")
                        .HasColumnType("bit");

                    b.Property<string>("Író")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Könyvek");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Előkölcsönzés", b =>
                {
                    b.HasOne("KönyvtárAlkalmazás.Models.Felhasználó", null)
                        .WithMany("Előkölcsönzések")
                        .HasForeignKey("FelhasználóId");

                    b.HasOne("KönyvtárAlkalmazás.Models.Könyv", "Könyv")
                        .WithMany()
                        .HasForeignKey("KönyvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Könyv");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Kölcsönzés", b =>
                {
                    b.HasOne("KönyvtárAlkalmazás.Models.Felhasználó", null)
                        .WithMany("Kölcsönzések")
                        .HasForeignKey("FelhasználóId");

                    b.HasOne("KönyvtárAlkalmazás.Models.Könyv", "Könyv")
                        .WithMany()
                        .HasForeignKey("KönyvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Könyv");
                });

            modelBuilder.Entity("KönyvtárAlkalmazás.Models.Felhasználó", b =>
                {
                    b.Navigation("Előkölcsönzések");

                    b.Navigation("Kölcsönzések");
                });
#pragma warning restore 612, 618
        }
    }
}
