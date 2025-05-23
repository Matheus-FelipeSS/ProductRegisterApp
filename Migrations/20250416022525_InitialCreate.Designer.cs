﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProductControl.Contexts;

#nullable disable

namespace ProductControl.Migrations
{
    [DbContext(typeof(ProductControlContext))]
    [Migration("20250416022525_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProductControl.Models.Loja", b =>
                {
                    b.Property<int>("IdLoja")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdLoja"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdLoja");

                    b.ToTable("Lojas");
                });

            modelBuilder.Entity("ProductControl.Models.Product", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdProduct"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DataEntrega")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("FinalizadoEm")
                        .HasColumnType("date");

                    b.Property<int>("IdLoja")
                        .HasColumnType("integer");

                    b.Property<int>("LojaIdLoja")
                        .HasColumnType("integer");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Validade")
                        .HasColumnType("date");

                    b.HasKey("IdProduct");

                    b.HasIndex("LojaIdLoja");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductControl.Models.Product", b =>
                {
                    b.HasOne("ProductControl.Models.Loja", "Loja")
                        .WithMany("Produtos")
                        .HasForeignKey("LojaIdLoja")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loja");
                });

            modelBuilder.Entity("ProductControl.Models.Loja", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
