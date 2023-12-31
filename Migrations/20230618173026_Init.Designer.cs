﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kolos2.Entities;

#nullable disable

namespace better.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230618173026_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("better.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Arnold",
                            LastName = "Boe"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Cristof",
                            LastName = "Zoe"
                        });
                });

            modelBuilder.Entity("better.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ClientID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FullfiledAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("StatusID");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientId = 1,
                            CreatedAt = new DateTime(2023, 6, 25, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            FullfiledAt = new DateTime(2023, 6, 28, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            ClientId = 2,
                            CreatedAt = new DateTime(2023, 6, 25, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("better.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Product 1",
                            Price = 10.539999999999999
                        },
                        new
                        {
                            Id = 2,
                            Name = "Product 2",
                            Price = 145.90000000000001
                        });
                });

            modelBuilder.Entity("better.Entities.ProductOrder", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ProductID");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("OrderID");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Product_Order");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            OrderId = 1,
                            Amount = 3
                        });
                });

            modelBuilder.Entity("better.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Finished"
                        });
                });

            modelBuilder.Entity("better.Entities.Order", b =>
                {
                    b.HasOne("better.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("better.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("better.Entities.ProductOrder", b =>
                {
                    b.HasOne("better.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("better.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
