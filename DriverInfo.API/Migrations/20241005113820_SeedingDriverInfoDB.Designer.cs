﻿// <auto-generated />
using DriverInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DriverInfo.API.Migrations
{
    [DbContext(typeof(DriverInfoContext))]
    [Migration("20241005113820_SeedingDriverInfoDB")]
    partial class SeedingDriverInfoDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("DriverInfo.API.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The guy who always wins.",
                            Name = "Max Verstappen"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Won F1-championship 7 times.",
                            Name = "Lewis Hamilton"
                        });
                });

            modelBuilder.Entity("DriverInfo.API.Entities.Win", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DriverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GridPosition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("Wins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DriverId = 1,
                            GridPosition = 4,
                            Name = "Spanish Grand Prix",
                            Year = 2016
                        },
                        new
                        {
                            Id = 2,
                            DriverId = 1,
                            GridPosition = 3,
                            Name = "Malaysian Grand Prix",
                            Year = 2017
                        },
                        new
                        {
                            Id = 3,
                            DriverId = 1,
                            GridPosition = 2,
                            Name = "Mexican Grand Prix",
                            Year = 2017
                        },
                        new
                        {
                            Id = 4,
                            DriverId = 2,
                            GridPosition = 1,
                            Name = "Canadian Grand Prix",
                            Year = 2007
                        },
                        new
                        {
                            Id = 5,
                            DriverId = 2,
                            GridPosition = 1,
                            Name = "United States Grand Prix",
                            Year = 2007
                        },
                        new
                        {
                            Id = 6,
                            DriverId = 2,
                            GridPosition = 1,
                            Name = "Hungarian Grand Prix",
                            Year = 2007
                        },
                        new
                        {
                            Id = 7,
                            DriverId = 2,
                            GridPosition = 1,
                            Name = "Japanese Grand Prix",
                            Year = 2007
                        });
                });

            modelBuilder.Entity("DriverInfo.API.Entities.Win", b =>
                {
                    b.HasOne("DriverInfo.API.Entities.Driver", "Driver")
                        .WithMany("Wins")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("DriverInfo.API.Entities.Driver", b =>
                {
                    b.Navigation("Wins");
                });
#pragma warning restore 612, 618
        }
    }
}
