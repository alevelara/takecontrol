using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddngSecurityStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("905df54f-f070-426c-83f9-0c9a0e25cbbc"), new Guid("d63fe7aa-a1da-44e9-8ae5-79d4b4296e3f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"), new Guid("d6a85134-d7ee-4089-8be3-ba4aec720c33") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"), new Guid("e6d1b6f2-ebce-489d-b0a8-00777a0a1a65") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cb4f3f21-665e-4b9d-84b5-1a4368ccead5"), new Guid("f4908fe8-2e73-4d64-a410-2826b382ce94") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("905df54f-f070-426c-83f9-0c9a0e25cbbc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cb4f3f21-665e-4b9d-84b5-1a4368ccead5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d63fe7aa-a1da-44e9-8ae5-79d4b4296e3f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d6a85134-d7ee-4089-8be3-ba4aec720c33"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e6d1b6f2-ebce-489d-b0a8-00777a0a1a65"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f4908fe8-2e73-4d64-a410-2826b382ce94"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"), null, "Player", "Player" },
                    { new Guid("905df54f-f070-426c-83f9-0c9a0e25cbbc"), null, "Club", "Club" },
                    { new Guid("cb4f3f21-665e-4b9d-84b5-1a4368ccead5"), null, "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("d63fe7aa-a1da-44e9-8ae5-79d4b4296e3f"), 0, "9fc4bc33-e6ac-4319-a71d-9858acb082ed", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAEF/5lm1+hQjucf1ZEaT7luAia/E+NSyiGIiCgSZ0mRcScSuKdG0yO47lmcRe3tV4xA==", null, false, null, false, "antgonmar2", 2 },
                    { new Guid("d6a85134-d7ee-4089-8be3-ba4aec720c33"), 0, "a1f154d2-d841-473a-b9da-12ac53af1937", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEFsi+cHILnxn/ElVTclp0wNB4GTZiId2PusRk0xIL/Mb680U9s1v+J6SUQgSHgfkQg==", null, false, null, false, "player2", 3 },
                    { new Guid("e6d1b6f2-ebce-489d-b0a8-00777a0a1a65"), 0, "baa81baa-98f0-4994-ae30-3ef58cea25ab", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEPrLH+vwKmXTFAnPL3Ifoh3etkOg4nBGsJ7YnBuM2OQGNVKo4HSrdnZF/Lo2unmkVw==", null, false, null, false, "antgonmar", 3 },
                    { new Guid("f4908fe8-2e73-4d64-a410-2826b382ce94"), 0, "0df25e73-ad1b-44a9-acae-2fc500543b2b", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEPgkEK65Xmj1Q0QJ1vLtR/IRs933WDP70g06umt0G+rCL6K/L+ZKAYj9uSJ6qTDC0g==", null, false, null, false, "alevelara", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("905df54f-f070-426c-83f9-0c9a0e25cbbc"), new Guid("d63fe7aa-a1da-44e9-8ae5-79d4b4296e3f") },
                    { new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"), new Guid("d6a85134-d7ee-4089-8be3-ba4aec720c33") },
                    { new Guid("04af47ab-2403-44fd-b8d8-852c2c71caeb"), new Guid("e6d1b6f2-ebce-489d-b0a8-00777a0a1a65") },
                    { new Guid("cb4f3f21-665e-4b9d-84b5-1a4368ccead5"), new Guid("f4908fe8-2e73-4d64-a410-2826b382ce94") }
                });
        }
    }
}
