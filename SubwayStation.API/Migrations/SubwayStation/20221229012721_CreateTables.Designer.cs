﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubwayStation.Domain;

#nullable disable

namespace SubwayStation.API.Migrations.SubwayStation
{
    [DbContext(typeof(SubwayStationContext))]
    [Migration("20221229012721_CreateTables")]
    partial class CreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SubwayStation.Domain.Entities.Frequently", b =>
                {
                    b.Property<int>("FrequentlyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FrequentlyId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubwayId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FrequentlyId");

                    b.HasIndex("SubwayId");

                    b.ToTable("Frequentlies");
                });

            modelBuilder.Entity("SubwayStation.Domain.Entities.Geometric", b =>
                {
                    b.Property<int>("GeometricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GeometricId"));

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GeometricId");

                    b.ToTable("Geometrics");
                });

            modelBuilder.Entity("SubwayStation.Domain.Entities.Subways", b =>
                {
                    b.Property<int>("ObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObjectId"));

                    b.Property<int>("GeometricId")
                        .HasColumnType("int");

                    b.Property<string>("Line")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObjectId");

                    b.HasIndex("GeometricId");

                    b.ToTable("Subways");
                });

            modelBuilder.Entity("SubwayStation.Domain.Entities.Frequently", b =>
                {
                    b.HasOne("SubwayStation.Domain.Entities.Subways", "Subways")
                        .WithMany()
                        .HasForeignKey("SubwayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subways");
                });

            modelBuilder.Entity("SubwayStation.Domain.Entities.Subways", b =>
                {
                    b.HasOne("SubwayStation.Domain.Entities.Geometric", "Geometric")
                        .WithMany()
                        .HasForeignKey("GeometricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Geometric");
                });
#pragma warning restore 612, 618
        }
    }
}
