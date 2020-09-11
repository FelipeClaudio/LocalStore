﻿// <auto-generated />
using System;
using LocalStore.Infrastructure.Database.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LocalStore.Infrastructure.Migrations.Product
{
    [DbContext(typeof(ProductContext))]
    [Migration("20200911002151_InitialProductMigration")]
    partial class InitialProductMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LocalStore.Infrastructure.Database.Products.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LocalStore.Infrastructure.Database.Products.Models.ProductPart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MeasuringUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductParts");
                });

            modelBuilder.Entity("LocalStore.Infrastructure.Database.Products.Models.ProductPart", b =>
                {
                    b.HasOne("LocalStore.Infrastructure.Database.Products.Models.Product", "Product")
                        .WithMany("ProductParts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("LocalStore.Infrastructure.Database.Products.Models.Material", "Material", b1 =>
                        {
                            b1.Property<Guid>("ProductPartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductPartId");

                            b1.ToTable("ProductParts");

                            b1.WithOwner()
                                .HasForeignKey("ProductPartId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
