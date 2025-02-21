﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250221205025_CambioRelacion")]
    partial class CambioRelacion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Entidades.DetalleFactura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Cantidad")
                        .HasColumnType("double precision");

                    b.Property<int>("FacturaId")
                        .HasColumnType("integer");

                    b.Property<double>("PrecioUnitario")
                        .HasColumnType("double precision");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Subtotal")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FacturaId");

                    b.ToTable("DetalleFacturas", (string)null);
                });

            modelBuilder.Entity("Data.Entidades.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Facturas", (string)null);
                });

            modelBuilder.Entity("Data.Entidades.DetalleFactura", b =>
                {
                    b.HasOne("Data.Entidades.Factura", "Facturas")
                        .WithMany("DetalleFacturas")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("Data.Entidades.Factura", b =>
                {
                    b.Navigation("DetalleFacturas");
                });
#pragma warning restore 612, 618
        }
    }
}
