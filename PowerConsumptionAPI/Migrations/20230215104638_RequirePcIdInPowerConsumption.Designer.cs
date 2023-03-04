﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerConsumptionAPI.Models;

#nullable disable

namespace PowerConsumptionAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230215104638_RequirePcIdInPowerConsumption")]
    partial class RequirePcIdInPowerConsumption
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PowerConsumptionAPI.Models.Computer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Computers");
                });

            modelBuilder.Entity("PowerConsumptionAPI.Models.PowerConsumption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ComputerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<float>("CpuPowerDraw")
                        .HasColumnType("float");

                    b.Property<float>("GpuPowerDraw")
                        .HasColumnType("float");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("PowerConsumptions");
                });

            modelBuilder.Entity("PowerConsumptionAPI.Models.PowerConsumption", b =>
                {
                    b.HasOne("PowerConsumptionAPI.Models.Computer", "Computer")
                        .WithMany("PowerConsumptionData")
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Computer");
                });

            modelBuilder.Entity("PowerConsumptionAPI.Models.Computer", b =>
                {
                    b.Navigation("PowerConsumptionData");
                });
#pragma warning restore 612, 618
        }
    }
}