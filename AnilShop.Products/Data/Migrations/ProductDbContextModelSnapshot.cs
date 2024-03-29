﻿// <auto-generated />
using System;
using AnilShop.Products;
using AnilShop.Products.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnilShop.Products.Data.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Products")
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnilShop.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("numeric(18,6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Products", "Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a89f6cd7-4693-457b-9009-02205dbbfe45"),
                            Description = "description",
                            Price = 10.99m,
                            Title = "title 1"
                        },
                        new
                        {
                            Id = new Guid("e4fa19bf-6981-4e50-a542-7c9b26e9ec31"),
                            Description = "description",
                            Price = 11.99m,
                            Title = "title 2"
                        },
                        new
                        {
                            Id = new Guid("17c61e41-3953-42cd-8f88-d3f698869b35"),
                            Description = "description",
                            Price = 12.99m,
                            Title = "title 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
