﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonstarHacks.Fugees.Backend;
using NetTopologySuite.Geometries;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    [DbContext(typeof(FugeesDbContext))]
    [Migration("20220402095022_addedHCPCertificateURI")]
    partial class addedHCPCertificateURI
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

                    b.Property<string>("CertificateURI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("UserId");

                    b.ToTable("HealthcareProfessionals");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.HealthcareProfessionalSpecialtyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SpecialityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HealthcareProfessionalSpecialtyTypes");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.MedicalSupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalSupplies");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.MedicalSupplyDonation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MedicalSuppliesId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MedicalSuppliesId");

                    b.HasIndex("UserId");

                    b.ToTable("MedicalSupplyDonations");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IdentityProviderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMedicalProfessional")
                        .HasColumnType("bit");

                    b.Property<Point>("LastKnownLocation")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.HealthcareProfessional", b =>
                {
                    b.HasOne("MonstarHacks.Fugees.Backend.Models.HealthcareProfessionalSpecialtyType", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MonstarHacks.Fugees.Backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MonstarHacks.Fugees.Backend.Models.MedicalSupplyDonation", b =>
                {
                    b.HasOne("MonstarHacks.Fugees.Backend.Models.MedicalSupply", "MedicalSupplies")
                        .WithMany()
                        .HasForeignKey("MedicalSuppliesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MonstarHacks.Fugees.Backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalSupplies");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
