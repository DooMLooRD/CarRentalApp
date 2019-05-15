﻿// <auto-generated />
using System;
using CarRentalApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalApi.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    partial class CarRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarRentalApi.Database.Model.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageURL");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Simple descr",
                            ImageURL = "Url",
                            Price = 10.199999999999999
                        },
                        new
                        {
                            Id = 2,
                            Description = "Des",
                            ImageURL = "Url1",
                            Price = 12.300000000000001
                        });
                });

            modelBuilder.Entity("CarRentalApi.Database.Model.Reservation", b =>
                {
                    b.Property<int>("ReservationNumber")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<int>("CarId");

                    b.Property<DateTime>("PickUpDate");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("ReservationNumber");

                    b.HasIndex("CarId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CarRentalApi.Database.Model.Reservation", b =>
                {
                    b.HasOne("CarRentalApi.Database.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("CarRentalApi.Database.Model.Location", "PickUpLocation", b1 =>
                        {
                            b1.Property<int>("ReservationNumber")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.HasKey("ReservationNumber");

                            b1.ToTable("Reservations");

                            b1.HasOne("CarRentalApi.Database.Model.Reservation")
                                .WithOne("PickUpLocation")
                                .HasForeignKey("CarRentalApi.Database.Model.Location", "ReservationNumber")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("CarRentalApi.Database.Model.Location", "ReturnLocation", b1 =>
                        {
                            b1.Property<int>("ReservationNumber")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.HasKey("ReservationNumber");

                            b1.ToTable("Reservations");

                            b1.HasOne("CarRentalApi.Database.Model.Reservation")
                                .WithOne("ReturnLocation")
                                .HasForeignKey("CarRentalApi.Database.Model.Location", "ReservationNumber")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
