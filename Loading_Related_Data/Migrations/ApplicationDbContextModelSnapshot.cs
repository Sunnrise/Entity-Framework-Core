﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Loading_Related_Data_Eager_Loading.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alperen",
                            RegionId = 1,
                            Salary = 1500,
                            Surname = "Güneş"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mehmet",
                            RegionId = 2,
                            Salary = 2000,
                            Surname = "Yılmaz"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ayşe",
                            RegionId = 3,
                            Salary = 2500,
                            Surname = "Kara"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fatma",
                            RegionId = 4,
                            Salary = 3000,
                            Surname = "Kara"
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6720)
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6735)
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6736)
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6737)
                        },
                        new
                        {
                            Id = 5,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6738)
                        },
                        new
                        {
                            Id = 6,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6739)
                        },
                        new
                        {
                            Id = 7,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6739)
                        },
                        new
                        {
                            Id = 8,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 7, 3, 11, 18, 32, 790, DateTimeKind.Local).AddTicks(6740)
                        });
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Marmara"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ege"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Karadeniz"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Akdeniz"
                        });
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasOne("Region", "Region")
                        .WithMany("Employees")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Region", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
