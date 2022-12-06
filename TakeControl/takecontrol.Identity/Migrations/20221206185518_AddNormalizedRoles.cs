using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"), new Guid("5c8c1b5d-a407-45d7-8905-f6540907206f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("78337060-9f1a-4fea-adf8-3a6697fb8177"), new Guid("ac1f186e-dbbd-4e3f-8fb9-cfb14914bd2c") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("21828f52-c104-4cf6-8e86-1da026b6453a"), new Guid("f0b508ac-d809-47d7-a226-925624aaf5c8") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"), new Guid("fbf0eec1-eeed-4be2-95ed-5d005a497588") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("21828f52-c104-4cf6-8e86-1da026b6453a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78337060-9f1a-4fea-adf8-3a6697fb8177"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5c8c1b5d-a407-45d7-8905-f6540907206f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ac1f186e-dbbd-4e3f-8fb9-cfb14914bd2c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0b508ac-d809-47d7-a226-925624aaf5c8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fbf0eec1-eeed-4be2-95ed-5d005a497588"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1907e862-c6be-46a5-b3ab-33c968de8445"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"), null, "Player", "PLAYER" },
                    { new Guid("99674349-639d-4ea7-b823-fb028a0f3a90"), null, "Club", "CLUB" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("1ec51cc4-4872-496b-8f7b-06729f00a241"), 0, "b88e3010-f9da-4e09-8255-7f783e237bb5", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEP2Kwm//yn4cGeuJ2Yc+xR/h0iySfOMp5mKBktrTLsHSy4Oxe2JOeqNRFzPAM4Ji9g==", null, false, "ffb82f3c-734e-4860-acc2-ea08e12ff04a", false, "antgonmar", 3 },
                    { new Guid("4936b787-55ff-4de7-8b22-45f4f3483286"), 0, "caad461e-5114-4b2b-b82e-495303a1cea2", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEFxtN/ALurY6b4rXdzQL4gR+RXWN8l/5EkY/mg8C9NC3CY1MaORfEZjN1LRQcpezSQ==", null, false, "d77b3262-b935-48b4-9039-2c9f74d55c79", false, "player2", 3 },
                    { new Guid("cd0f7611-708a-4a0b-ba80-3e3076df60e4"), 0, "c256d728-1f85-47ae-a8d4-372973a44866", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAECpne90h2KRvunZwKKk+H1cTAhMRtIomoR49dvxAlzB6UwASMKISeHmaDYpEwGQQxQ==", null, false, "6c3cb562-2735-40cc-838e-abbe4e096720", false, "antgonmar2", 2 },
                    { new Guid("fe9f0b77-c816-4808-a1ac-fbdfa1d71e31"), 0, "27e936fe-98fd-4d71-9633-8f53f6da0a13", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEFStHXgmkb/bxJq9oKW2HAdIutO6OPqVJIjpUx/5gOhS+21lubU29otE11YPtjWdMg==", null, false, "a2a3643d-d918-4492-8865-c15cdcb6b348", false, "alevelara", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"), new Guid("1ec51cc4-4872-496b-8f7b-06729f00a241") },
                    { new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"), new Guid("4936b787-55ff-4de7-8b22-45f4f3483286") },
                    { new Guid("99674349-639d-4ea7-b823-fb028a0f3a90"), new Guid("cd0f7611-708a-4a0b-ba80-3e3076df60e4") },
                    { new Guid("1907e862-c6be-46a5-b3ab-33c968de8445"), new Guid("fe9f0b77-c816-4808-a1ac-fbdfa1d71e31") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"), new Guid("1ec51cc4-4872-496b-8f7b-06729f00a241") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"), new Guid("4936b787-55ff-4de7-8b22-45f4f3483286") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("99674349-639d-4ea7-b823-fb028a0f3a90"), new Guid("cd0f7611-708a-4a0b-ba80-3e3076df60e4") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1907e862-c6be-46a5-b3ab-33c968de8445"), new Guid("fe9f0b77-c816-4808-a1ac-fbdfa1d71e31") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1907e862-c6be-46a5-b3ab-33c968de8445"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("776e5ee0-ff44-48c9-991c-ca7062d5ab3a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("99674349-639d-4ea7-b823-fb028a0f3a90"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1ec51cc4-4872-496b-8f7b-06729f00a241"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4936b787-55ff-4de7-8b22-45f4f3483286"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cd0f7611-708a-4a0b-ba80-3e3076df60e4"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe9f0b77-c816-4808-a1ac-fbdfa1d71e31"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"), null, "Player", "Player" },
                    { new Guid("21828f52-c104-4cf6-8e86-1da026b6453a"), null, "Club", "Club" },
                    { new Guid("78337060-9f1a-4fea-adf8-3a6697fb8177"), null, "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("5c8c1b5d-a407-45d7-8905-f6540907206f"), 0, "617e1a9b-0993-4eb7-96e0-a27c02e8fa4d", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEAI//ayoaU2T3Ltl2zf0jVdcUzzNXqqQquCPccrtu2RNjs4tmGxNfyMqZV/d3QrRnw==", null, false, "14295d6f-d242-463a-bc3f-08dca6d14894", false, "antgonmar", 3 },
                    { new Guid("ac1f186e-dbbd-4e3f-8fb9-cfb14914bd2c"), 0, "9d1ab493-7826-4f69-ab86-d7e7c397cd62", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEDuI2DZJM8O4TZFDMmj1UxRCySg1Y0P/qxxICkR4UswGueREF3Nm90rY9qrxaeAxeg==", null, false, "bc418660-81ec-4068-bc11-7c3ed4d34751", false, "alevelara", 1 },
                    { new Guid("f0b508ac-d809-47d7-a226-925624aaf5c8"), 0, "0d8dbda2-82c5-4e69-bc52-04b86d30c280", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAEA3rl4hz1w64dsuOYl8KjglWOEI6pkim/B1dANb5cjEH+hsY9nNvwh2OEJq6iOpaNA==", null, false, "df278af4-dae0-444c-8f66-aae0f83b9e12", false, "antgonmar2", 2 },
                    { new Guid("fbf0eec1-eeed-4be2-95ed-5d005a497588"), 0, "caf9d569-01db-4f5b-b2c6-ba9bcc77e2cc", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAELZDm6+ZqtbBY0ME4JNHmqbZU3oN+kMoQeeRQ80j+LMSUKwVW+Ee/gsiM40JlXhYkQ==", null, false, "9c33546a-0e4e-48ab-9f70-9fb5d467fb0a", false, "player2", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"), new Guid("5c8c1b5d-a407-45d7-8905-f6540907206f") },
                    { new Guid("78337060-9f1a-4fea-adf8-3a6697fb8177"), new Guid("ac1f186e-dbbd-4e3f-8fb9-cfb14914bd2c") },
                    { new Guid("21828f52-c104-4cf6-8e86-1da026b6453a"), new Guid("f0b508ac-d809-47d7-a226-925624aaf5c8") },
                    { new Guid("16177e60-2a44-485e-ab55-1aaaf0cb11a8"), new Guid("fbf0eec1-eeed-4be2-95ed-5d005a497588") }
                });
        }
    }
}
