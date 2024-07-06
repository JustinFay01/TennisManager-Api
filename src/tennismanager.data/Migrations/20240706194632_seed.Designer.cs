﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tennismanager.data;

#nullable disable

namespace tennismanager.data.Migrations
{
    [DbContext(typeof(TennisManagerContext))]
    [Migration("20240706194632_seed")]
    partial class seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("tennismanager.data.Entities.Abstract.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("tennismanager.data.Entities.CoachPackagePrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PackageId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("CoachPackagePrices");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CustomerPackage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PackageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UsesRemaining")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PackageId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("CustomerPackages");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CustomerSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SessionId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("CustomerSessions");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DefaultPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Uses")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Sessions");

                    b.HasDiscriminator<string>("Type").HasValue("PickleHitting");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("tennismanager.data.Entities.Coach", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Abstract.User");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Customer", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Abstract.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("tennismanager.data.Entities.PrivateSession", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Session");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uuid");

                    b.HasIndex("CoachId");

                    b.HasDiscriminator().HasValue("PicklePrivate");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CoachPackagePrice", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Coach", "Coach")
                        .WithMany("PackagePricesList")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Package", "Package")
                        .WithMany("PackagePricesList")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Coach");

                    b.Navigation("CreatedBy");

                    b.Navigation("Package");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CustomerPackage", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Abstract.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Customer", "Customer")
                        .WithMany("Packages")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Package", "Package")
                        .WithMany("Customers")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Customer");

                    b.Navigation("Package");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CustomerSession", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Abstract.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Customer", "Customer")
                        .WithMany("ParticipatedSessions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Session", "Session")
                        .WithMany("CustomerSessions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Customer");

                    b.Navigation("Session");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Package", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Abstract.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Abstract.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("tennismanager.data.Entities.PrivateSession", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Package", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("PackagePricesList");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.Navigation("CustomerSessions");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Coach", b =>
                {
                    b.Navigation("PackagePricesList");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Customer", b =>
                {
                    b.Navigation("Packages");

                    b.Navigation("ParticipatedSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
