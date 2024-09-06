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
    [Migration("20240906233639_intervalToLong")]
    partial class intervalToLong
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

            modelBuilder.Entity("tennismanager.data.Entities.CustomerSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("CustomPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SessionId");

                    b.ToTable("CustomerSessions");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Group", b =>
                {
                    b.Property<int>("MemberNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MemberNumber"));

                    b.HasKey("MemberNumber");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Package", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Uses")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CoachId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("tennismanager.data.Entities.SessionInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("RepeatInterval")
                        .HasColumnType("bigint");

                    b.Property<Guid>("SessionMetaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SessionMetaId");

                    b.ToTable("SessionIntervals");
                });

            modelBuilder.Entity("tennismanager.data.Entities.SessionMeta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Recurring")
                        .HasColumnType("boolean");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer");

                    b.Property<Guid>("SessionId1")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SessionId1");

                    b.ToTable("SessionMetas");
                });

            modelBuilder.Entity("tennismanager.data.Entities.UserGroup", b =>
                {
                    b.Property<int>("MemberNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("GroupMemberNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MemberNumber", "UserId");

                    b.HasIndex("GroupMemberNumber");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Abstract.Admin", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Abstract.User");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Coach", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Abstract.User");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("numeric");

                    b.HasDiscriminator().HasValue("Coach");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Customer", b =>
                {
                    b.HasBaseType("tennismanager.data.Entities.Abstract.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("tennismanager.data.Entities.CustomerSession", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Customer", "Customer")
                        .WithMany("Sessions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Session", "Session")
                        .WithMany("CustomerSessions")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Package", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Coach", "Coach")
                        .WithMany()
                        .HasForeignKey("CoachId");

                    b.Navigation("Coach");
                });

            modelBuilder.Entity("tennismanager.data.Entities.SessionInterval", b =>
                {
                    b.HasOne("tennismanager.data.Entities.SessionMeta", "SessionMeta")
                        .WithMany()
                        .HasForeignKey("SessionMetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SessionMeta");
                });

            modelBuilder.Entity("tennismanager.data.Entities.SessionMeta", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("tennismanager.data.Entities.UserGroup", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupMemberNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tennismanager.data.Entities.Abstract.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Session", b =>
                {
                    b.Navigation("CustomerSessions");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Customer", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
