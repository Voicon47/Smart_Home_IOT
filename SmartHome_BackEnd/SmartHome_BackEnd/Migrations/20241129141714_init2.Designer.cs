﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHome_BackEnd.Data;

#nullable disable

namespace SmartHome_BackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241129141714_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartHome_BackEnd.Entities.Sensor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("Humidity")
                        .HasColumnType("float");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
