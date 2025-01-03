﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tennismanager.data;

#nullable disable

namespace tennismanager.data.Migrations
{
    [DbContext(typeof(TennisManagerContext))]
    partial class TennisManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
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

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Picture")
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

            modelBuilder.Entity("tennismanager.data.Entities.Events.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("IsFullDay")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRecurring")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ParentEventId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParentEventId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Events.RecurringPattern", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("DayOfMonth")
                        .HasColumnType("integer");

                    b.Property<int?>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<int?>("MaxOccurrences")
                        .HasColumnType("integer");

                    b.Property<int?>("MonthOfYear")
                        .HasColumnType("integer");

                    b.Property<int>("RecurringType")
                        .HasColumnType("integer");

                    b.Property<int>("SeparationCount")
                        .HasColumnType("integer");

                    b.Property<int?>("WeekOfMonth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("RecurringPattern");
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

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("EventId");

                    b.ToTable("Sessions");
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

            modelBuilder.Entity("tennismanager.data.Entities.Events.Event", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Events.Event", "ParentEvent")
                        .WithMany()
                        .HasForeignKey("ParentEventId");

                    b.Navigation("ParentEvent");
                });

            modelBuilder.Entity("tennismanager.data.Entities.Events.RecurringPattern", b =>
                {
                    b.HasOne("tennismanager.data.Entities.Events.Event", "Event")
                        .WithMany("RecurringPatterns")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
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

                    b.HasOne("tennismanager.data.Entities.Events.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Event");
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

            modelBuilder.Entity("tennismanager.data.Entities.Events.Event", b =>
                {
                    b.Navigation("RecurringPatterns");
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
