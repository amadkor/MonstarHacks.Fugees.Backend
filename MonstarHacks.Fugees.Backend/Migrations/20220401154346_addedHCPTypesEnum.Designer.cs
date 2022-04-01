﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonstarHacks.Fugees.Backend;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    [DbContext(typeof(FugeesDbContext))]
    [Migration("20220401154346_addedHCPTypesEnum")]
    partial class addedHCPTypesEnum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.HealthcareProfessional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SpecialityName")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HealthcareProfessionals");
                });
#pragma warning restore 612, 618
        }
    }
}