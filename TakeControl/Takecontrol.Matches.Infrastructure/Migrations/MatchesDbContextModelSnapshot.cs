﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;

#nullable disable

namespace Takecontrol.Matches.Infrastructure.Migrations
{
    [DbContext(typeof(MatchesDbContext))]
    partial class MatchesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Courts.Court", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClubId")
                        .HasColumnType("uuid");

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
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("courts", (string)null);
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.MatchPlayers.MatchPlayer", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MatchId")
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

                    b.HasKey("PlayerId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("matches_players", (string)null);
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("matches", (string)null);
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Reservations.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourtId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeOnly>("EndDate")
                        .HasColumnType("time without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("ReservationDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("StartDate")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.MatchPlayers.MatchPlayer", b =>
                {
                    b.HasOne("Takecontrol.Matches.Domain.Models.Matches.Match", "Match")
                        .WithMany("MatchPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Matches.Match", b =>
                {
                    b.HasOne("Takecontrol.Matches.Domain.Models.Reservations.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Reservations.Reservation", b =>
                {
                    b.HasOne("Takecontrol.Matches.Domain.Models.Courts.Court", "Court")
                        .WithMany("Reservations")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Courts.Court", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Takecontrol.Matches.Domain.Models.Matches.Match", b =>
                {
                    b.Navigation("MatchPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
