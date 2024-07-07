﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Concurrency.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240707152747_mig1")]
    partial class mig1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Name = "Ayşe"
                        },
                        new
                        {
                            PersonId = 2,
                            Name = "Hilmi"
                        },
                        new
                        {
                            PersonId = 3,
                            Name = "Raziye"
                        },
                        new
                        {
                            PersonId = 4,
                            Name = "Süleyman"
                        },
                        new
                        {
                            PersonId = 5,
                            Name = "Fadime"
                        },
                        new
                        {
                            PersonId = 6,
                            Name = "Şuayip"
                        },
                        new
                        {
                            PersonId = 7,
                            Name = "Lale"
                        },
                        new
                        {
                            PersonId = 8,
                            Name = "Jale"
                        },
                        new
                        {
                            PersonId = 9,
                            Name = "Rıfkı"
                        },
                        new
                        {
                            PersonId = 10,
                            Name = "Muaviye"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
