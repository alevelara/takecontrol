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
                            Id = new Guid("e3544b23-7228-439b-ab5e-c8de350e32c9"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("3bbf6101-6c33-4799-96d4-9b1fe0c7bb1c"),
                            Name = "Player",
                            NormalizedName = "PLAYER"
                        },
                        new
                        {
                            Id = new Guid("59989d96-301c-46be-a1ff-eb62d3f45060"),
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
                            UserId = new Guid("5ef1cdd0-433c-4467-b891-f322062589c8"),
                            RoleId = new Guid("e3544b23-7228-439b-ab5e-c8de350e32c9")
                        },
                        new
                        {
                            UserId = new Guid("cc32b57c-4924-4622-8f49-2c639544293d"),
                            RoleId = new Guid("59989d96-301c-46be-a1ff-eb62d3f45060")
                        },
                        new
                        {
                            UserId = new Guid("f7da2663-aeae-4528-b2a2-acfe9659953c"),
                            RoleId = new Guid("3bbf6101-6c33-4799-96d4-9b1fe0c7bb1c")
                        },
                        new
                        {
                            UserId = new Guid("54bec211-025d-4dc3-8783-9ba712f113bf"),
                            RoleId = new Guid("3bbf6101-6c33-4799-96d4-9b1fe0c7bb1c")
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
                            Id = new Guid("5ef1cdd0-433c-4467-b891-f322062589c8"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ad25e348-4b2f-4991-9268-9ec76f60e803",
                            Email = "alevelara@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alejandro",
                            NormalizedEmail = "ALEVELARA@GMAIL.COM",
                            NormalizedUserName = "ALEVELARA",
                            PasswordHash = "AQAAAAIAAYagAAAAEFqKEQrmdIGB3srhwgqyGiXuF0H5fjYM3GYzrUTumZ7rB1J0u/M0AEVn1LEEWJajTA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2b59aa63-616b-4fd4-8ec8-852b9c51303a",
                            TwoFactorEnabled = false,
                            UserName = "alevelara",
                            UserType = 1
                        },
                        new
                        {
                            Id = new Guid("f7da2663-aeae-4528-b2a2-acfe9659953c"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0934d487-f19e-4471-a969-a732a616859e",
                            Email = "alevelara@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alberto",
                            NormalizedEmail = "ALEVELARA@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR",
                            PasswordHash = "AQAAAAIAAYagAAAAEOjiLBtbrUwAkZXHcsYQXRQ3DX6zwXwERzPmTf3rZRIV+4+3ZvprztHzrAQOUSDsNw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "13b131c2-113a-4572-99ee-7fce7d933cad",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar",
                            UserType = 3
                        },
                        new
                        {
                            Id = new Guid("cc32b57c-4924-4622-8f49-2c639544293d"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d319ae0a-c74b-481d-a40c-02101946e75e",
                            Email = "club@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "PadelClubTest",
                            NormalizedEmail = "CLUB@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR2",
                            PasswordHash = "AQAAAAIAAYagAAAAEErPvCt0xp8N/n+lJgA7anIsYvgr8BTKAFHXF82NazCeUp7VmgavZj52Spwkk/Kl1g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ce4de9a1-1767-4fcb-9df9-322b576937b6",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar2",
                            UserType = 2
                        },
                        new
                        {
                            Id = new Guid("54bec211-025d-4dc3-8783-9ba712f113bf"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "569db19e-6782-4046-81fb-ea18098d2078",
                            Email = "player2@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "player 2",
                            NormalizedEmail = "PLAYER2@GMAIL.COM",
                            NormalizedUserName = "PLAYER2",
                            PasswordHash = "AQAAAAIAAYagAAAAEDxGuF7A2KPwB4ABzSGxvdigsfUoorBZqYeAwkGxnIPqv4BRFh5lsd29UnuoUH37bw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e240a734-1a1c-40da-8cc1-cef884acbc0d",
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
