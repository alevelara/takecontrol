﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Takecontrol.Credential.Infrastructure.Contexts;

#nullable disable

namespace Takecontrol.Credential.Infrastructure.Migrations
{
    [DbContext(typeof(TakeControlIdentityDbContext))]
    partial class TakeControlIdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
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
                            Id = new Guid("73fec235-1fae-4967-94fb-9bb7f58481cc"),
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("301c4234-32ad-4ae2-9355-e3c4724a2fbf"),
                            Name = "Player",
                            NormalizedName = "PLAYER"
                        },
                        new
                        {
                            Id = new Guid("e10d8f53-6815-4033-9549-b8830768095e"),
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
                            UserId = new Guid("1967b188-df7f-4330-9b83-bd852dc71769"),
                            RoleId = new Guid("73fec235-1fae-4967-94fb-9bb7f58481cc")
                        },
                        new
                        {
                            UserId = new Guid("f7c5660b-0e98-4a06-b557-ce7ed7ce9a62"),
                            RoleId = new Guid("e10d8f53-6815-4033-9549-b8830768095e")
                        },
                        new
                        {
                            UserId = new Guid("bc5b19a5-8fdc-48db-b3b0-65d9685ca666"),
                            RoleId = new Guid("301c4234-32ad-4ae2-9355-e3c4724a2fbf")
                        },
                        new
                        {
                            UserId = new Guid("7face85d-d05d-4645-a348-52f070a3c0eb"),
                            RoleId = new Guid("301c4234-32ad-4ae2-9355-e3c4724a2fbf")
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

            modelBuilder.Entity("Takecontrol.Credential.Infrastructure.Models.ApplicationUser", b =>
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
                            Id = new Guid("1967b188-df7f-4330-9b83-bd852dc71769"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "43811fdf-66a6-44d3-9811-2082cf5b4bb1",
                            Email = "alevelara@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alejandro",
                            NormalizedEmail = "ALEVELARA@GMAIL.COM",
                            NormalizedUserName = "ALEVELARA",
                            PasswordHash = "AQAAAAIAAYagAAAAEGeI/n/YzwWqrOf56WjXV0irscecxsUvpob8eXbhneGLlN4hfvwwLu5xbSnxDsCNoQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1b739782-f142-4cfd-8b1e-e73e2f0422a8",
                            TwoFactorEnabled = false,
                            UserName = "alevelara",
                            UserType = 1
                        },
                        new
                        {
                            Id = new Guid("bc5b19a5-8fdc-48db-b3b0-65d9685ca666"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "40761f19-ada6-4f49-8c79-a70675faa741",
                            Email = "alevelara@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Alberto",
                            NormalizedEmail = "ALEVELARA@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR",
                            PasswordHash = "AQAAAAIAAYagAAAAECH/+BXjXYHCYDHf3FuptYFPMmV01Mo1+DqcHobfkwZ4us8wUbbN1h2wjYBBvaosFg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7aaf9292-c110-4daf-8272-4da6fa45cdbb",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar",
                            UserType = 3
                        },
                        new
                        {
                            Id = new Guid("f7c5660b-0e98-4a06-b557-ce7ed7ce9a62"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ca6e194d-4b95-45b7-995a-3aeac7145baa",
                            Email = "club@localhost.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "PadelClubTest",
                            NormalizedEmail = "CLUB@LOCALHOST.COM",
                            NormalizedUserName = "ANTOGONMAR2",
                            PasswordHash = "AQAAAAIAAYagAAAAEJ1ztyZMxNbYQaYpGwWPwL9vcuPHcfTSIQI5rpUkiIFgNS5nVagYi90Nitwis7HIWg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f914df0b-b1d3-43e0-a7d1-000b70224353",
                            TwoFactorEnabled = false,
                            UserName = "antgonmar2",
                            UserType = 2
                        },
                        new
                        {
                            Id = new Guid("7face85d-d05d-4645-a348-52f070a3c0eb"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "87f7b224-c518-46bd-b533-e60aa051e82a",
                            Email = "player2@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "player 2",
                            NormalizedEmail = "PLAYER2@GMAIL.COM",
                            NormalizedUserName = "PLAYER2",
                            PasswordHash = "AQAAAAIAAYagAAAAEAqc97WpuQ1/TFAzUY6ro0xHrfx7rDB0CKOUuQAUvE47Dbu9ow9JbOU3eQC9XvU0YA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ce7e0734-9bd6-4e95-a6ec-bd577b3fd7ad",
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
                    b.HasOne("Takecontrol.Credential.Infrastructure.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Takecontrol.Credential.Infrastructure.Models.ApplicationUser", null)
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

                    b.HasOne("Takecontrol.Credential.Infrastructure.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Takecontrol.Credential.Infrastructure.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
