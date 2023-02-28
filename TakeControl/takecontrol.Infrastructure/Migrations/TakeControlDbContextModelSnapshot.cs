﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using takecontrol.Identity;

#nullable disable

namespace takecontrol.Infrastructure.Migrations
{
    [DbContext(typeof(TakeControlDbContext))]
    partial class TakeControlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("takecontrol.Domain.Models.Addresses.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MainAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Clubs.Club", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddresId")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddresId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("clubs", (string)null);
                });

            modelBuilder.Entity("takecontrol.Domain.Models.PlayerClubs.PlayerClub", b =>
                {
                    b.Property<Guid>("ClubId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ClubId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerClubs");
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Players.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AvgNumberOfMatchesInAWeek")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("NumberOfClassesInAWeek")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfYearsPlayed")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerLevel")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("players", (string)null);
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Clubs.Club", b =>
                {
                    b.HasOne("takecontrol.Domain.Models.Addresses.Address", "Address")
                        .WithOne("Club")
                        .HasForeignKey("takecontrol.Domain.Models.Clubs.Club", "AddresId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("takecontrol.Domain.Models.PlayerClubs.PlayerClub", b =>
                {
                    b.HasOne("takecontrol.Domain.Models.Clubs.Club", "Club")
                        .WithMany("PlayerClubs")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("takecontrol.Domain.Models.Players.Player", "Player")
                        .WithMany("PlayerClubs")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Addresses.Address", b =>
                {
                    b.Navigation("Club")
                        .IsRequired();
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Clubs.Club", b =>
                {
                    b.Navigation("PlayerClubs");
                });

            modelBuilder.Entity("takecontrol.Domain.Models.Players.Player", b =>
                {
                    b.Navigation("PlayerClubs");
                });
#pragma warning restore 612, 618
        }
    }
}
