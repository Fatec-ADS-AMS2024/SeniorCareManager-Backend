﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SeniorCareManager.WebAPI.Data;

#nullable disable

namespace SeniorCareManager.WebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240921211604_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SeniorCareManager.WebAPI.Objects.Models.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("productgroup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Medicamentos"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Equipamentos Médicos"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Suplementos"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
