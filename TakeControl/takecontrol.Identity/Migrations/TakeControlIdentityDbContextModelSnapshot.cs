﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using takecontrol.Identity;

#nullable disable

namespace takecontrol.Identity.Migrations
{
    [DbContext(typeof(TakeControlIdentityDbContext))]
    partial class TakeControlIdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("48e304e1-b820-4865-8189-f6dc00877022"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("3ca33cb3-c0a4-4309-822a-9c75abc79f7c"),
                            Name = "Player",
                            NormalizedName = "PLAYER"
                        },
                        new
                        {
                            Id = new Guid("223de600-5321-468d-8382-d320c1de07ee"),
                            Name = "Club",
                            NormalizedName = "CLUB"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e0ebfbcc-2d94-49e3-a16b-0f31c2e86375"),
                            RoleId = new Guid("48e304e1-b820-4865-8189-f6dc00877022")
                        },
                        new
                        {
                            UserId = new Guid("8f4c1060-89a4-478e-9a32-d0b30f6e869b"),
                            RoleId = new Guid("223de600-5321-468d-8382-d320c1de07ee")
                        },
                        new
                        {
                            UserId = new Guid("a2f90331-cf0f-4172-96f1-509b4e8079e5"),
                            RoleId = new Guid("3ca33cb3-c0a4-4309-822a-9c75abc79f7c")
                        },
                        new
                        {
                            UserId = new Guid("41432b39-4f42-4582-bcdf-c10aa47bdc10"),
                            RoleId = new Guid("3ca33cb3-c0a4-4309-822a-9c75abc79f7c")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("takecontrol.Identity.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0ebfbcc-2d94-49e3-a16b-0f31c2e86375"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3a10e558-0cf2-4d69-a65a-b5b930234319",
                            Email = "alevelara@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alejandro",
                            NormalizedEmail = "ALEVELARA@GMAIL.COM",
                            NormalizedUserName = "ALEVELARA",
                            PasswordHash = "AQAAAAIAAYagAAAAEICm7yvaUlTeGC6VF7jOdYXIl4JDVJ42u3tGwDN3EntxwEGLDyFJ8pvwi8J1gIXALg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "50f20fd4-70bc-4c80-98de-23eac36cd095",
                            TwoFactorEnabled = false,
                            UserName = "alevelara",
                            UserType = 1
                        },
                        new
                        {
                            Id = new Guid("a2f90331-cf0f-4172-96f1-509b4e8079e5"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c4b3687f-973f-49d6-a930-35c0aa22b997",
                            Email = "alevelara@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alberto",
                            NormalizedEmail = "ALEVELARA@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR",
                            PasswordHash = "AQAAAAIAAYagAAAAEJGQiDrKbqXK79DRDlS3JDKl5rDCbpzkYDaG+85sdvvgoHsIx/GRCTidLSJp4ijFPg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8a87d146-9ff9-4f7c-81be-1c9ed730675e",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar",
                            UserType = 3
                        },
                        new
                        {
                            Id = new Guid("8f4c1060-89a4-478e-9a32-d0b30f6e869b"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "257a7912-310b-4135-903e-710c701c6f8a",
                            Email = "club@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "PadelClubTest",
                            NormalizedEmail = "CLUB@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR2",
                            PasswordHash = "AQAAAAIAAYagAAAAEPvXlW6hiwRUvxbG+zRtb27T0lKmr6GJU3WlyK5y63ZgMW3MSeBjTV5dWzTP9EaPvQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "227ef87e-d26e-4015-993d-b416d51afb70",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar2",
                            UserType = 2
                        },
                        new
                        {
                            Id = new Guid("41432b39-4f42-4582-bcdf-c10aa47bdc10"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8b07d38f-b860-4365-acbc-dc6288fead60",
                            Email = "player2@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "player 2",
                            NormalizedEmail = "PLAYER2@GMAIL.COM",
                            NormalizedUserName = "PLAYER2",
                            PasswordHash = "AQAAAAIAAYagAAAAEBMZ3lzAR6V8bhZnWPoMEwoeJG2rxaoI4BHEXlRvX9WNovZtsVfeKub5YXIUktB+cw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "90ce20bd-5abe-440f-b7b8-50b682d971a8",
                            TwoFactorEnabled = false,
                            UserName = "player2",
                            UserType = 3
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("takecontrol.Identity.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("takecontrol.Identity.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("takecontrol.Identity.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("takecontrol.Identity.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
