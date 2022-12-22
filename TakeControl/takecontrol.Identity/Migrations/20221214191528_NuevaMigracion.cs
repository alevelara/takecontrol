using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d6db5a4f-9ccc-4095-a348-c117135ad788"), new Guid("1826f84a-0b71-4e65-a8ed-e638023b20da") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("11d5e188-78c3-4231-a283-ace535c72f68"), new Guid("25e4f5c1-3c68-4f9e-8b37-071d3c9b166f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c0093646-16d0-4b63-8ce1-389ec38d21ad"), new Guid("5110c289-7fbc-42bd-a77a-bf6428b3288d") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("11d5e188-78c3-4231-a283-ace535c72f68"), new Guid("d2e78011-6cb3-45a8-b330-36e963581333") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11d5e188-78c3-4231-a283-ace535c72f68"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c0093646-16d0-4b63-8ce1-389ec38d21ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d6db5a4f-9ccc-4095-a348-c117135ad788"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1826f84a-0b71-4e65-a8ed-e638023b20da"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("25e4f5c1-3c68-4f9e-8b37-071d3c9b166f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5110c289-7fbc-42bd-a77a-bf6428b3288d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2e78011-6cb3-45a8-b330-36e963581333"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9afe48f3-6e13-4fd6-be43-cdaecf4f2674"), null, "Club", "CLUB" },
                    { new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"), null, "Player", "PLAYER" },
                    { new Guid("c35472be-9e6e-41d6-a2a5-14b46df8a4aa"), null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("3e3acdb3-bcee-4d52-bbe0-4cc8e73142ca"), 0, "981da0bb-2412-4b8e-92b2-b10f4521a5f2", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEFVwpY1lksvCkrcuZ7ERy0VuIKWhB6V2z/PrwSx/GQBH3ugkbSnEDnIX30T3ZU7ODA==", null, false, "7ef157ff-51fe-48fb-8eb5-5f807dda6823", false, "alevelara", 1 },
                    { new Guid("53615aac-95ec-4354-8350-ff75f1351107"), 0, "7ffe0d79-27f0-408b-b3b2-9159c9b4de5b", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEKccyLPoQJUM/4RwKR1YPdKDtjMQ4xzirDnzrQCxaSwR27mODdiFO0e7QV1uZKGHIw==", null, false, "612b4a89-c7c2-4f9e-8f40-49d93b38eb2e", false, "antgonmar", 3 },
                    { new Guid("6d5dc714-11a4-4d1c-8bb3-b668cbea0f5e"), 0, "0a7e1bd8-0666-4c13-96b8-d2a56c189639", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAEN8/aI5B+SCsqmyODNgVVdaMTdwzLBMyM4WPvH4J8ScRSfDdVFa6PmUXRtLDC8MLPQ==", null, false, "7b4021e8-e202-4006-b2ac-f9f52274ed6b", false, "antgonmar2", 2 },
                    { new Guid("f9eefd29-b6b3-4517-9613-95b238540240"), 0, "bdb10e0a-a8ce-425d-a699-92b09266257a", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEJU/5a4rrmxvU5D5MwcQ1I1aNTWSvmk01Y8jC4suDsz8op/iRVSAFVY6QyjMNJ8k6Q==", null, false, "8419ef35-8ef0-4c2e-b888-1811f8b6d289", false, "player2", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c35472be-9e6e-41d6-a2a5-14b46df8a4aa"), new Guid("3e3acdb3-bcee-4d52-bbe0-4cc8e73142ca") },
                    { new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"), new Guid("53615aac-95ec-4354-8350-ff75f1351107") },
                    { new Guid("9afe48f3-6e13-4fd6-be43-cdaecf4f2674"), new Guid("6d5dc714-11a4-4d1c-8bb3-b668cbea0f5e") },
                    { new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"), new Guid("f9eefd29-b6b3-4517-9613-95b238540240") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c35472be-9e6e-41d6-a2a5-14b46df8a4aa"), new Guid("3e3acdb3-bcee-4d52-bbe0-4cc8e73142ca") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"), new Guid("53615aac-95ec-4354-8350-ff75f1351107") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9afe48f3-6e13-4fd6-be43-cdaecf4f2674"), new Guid("6d5dc714-11a4-4d1c-8bb3-b668cbea0f5e") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"), new Guid("f9eefd29-b6b3-4517-9613-95b238540240") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9afe48f3-6e13-4fd6-be43-cdaecf4f2674"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bb37520a-638c-443f-8988-cd656a9e92ff"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c35472be-9e6e-41d6-a2a5-14b46df8a4aa"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3e3acdb3-bcee-4d52-bbe0-4cc8e73142ca"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53615aac-95ec-4354-8350-ff75f1351107"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5dc714-11a4-4d1c-8bb3-b668cbea0f5e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f9eefd29-b6b3-4517-9613-95b238540240"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11d5e188-78c3-4231-a283-ace535c72f68"), null, "Player", "PLAYER" },
                    { new Guid("c0093646-16d0-4b63-8ce1-389ec38d21ad"), null, "Administrator", "ADMINISTRATOR" },
                    { new Guid("d6db5a4f-9ccc-4095-a348-c117135ad788"), null, "Club", "CLUB" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("1826f84a-0b71-4e65-a8ed-e638023b20da"), 0, "2d9d76c6-8a15-44e9-9ffa-66fe53a0fe1c", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAEI8QfUKx7FRQAWRNpKkihY881/sK8L/GvkPHCHPPdnUECNLk9CaGTWTh9eaJrHfgvw==", null, false, "838a7d09-368f-4492-8a44-f46734f69630", false, "antgonmar2", 2 },
                    { new Guid("25e4f5c1-3c68-4f9e-8b37-071d3c9b166f"), 0, "2a3ab53a-95f9-4032-b946-8d8fb8210d28", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEOzZWjdqEiGeZM9fTY9FG6MXmrt2atexaUfvxaXSo0LvWVFM+aomy1+xzz0jA/UYvA==", null, false, "5e9e9212-6512-44ae-bb79-73553b03336a", false, "antgonmar", 3 },
                    { new Guid("5110c289-7fbc-42bd-a77a-bf6428b3288d"), 0, "a6cdaee8-5c95-4f5d-af08-9961f15f7f0d", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEOtAtsrLATJJJ8fOU/onYEcVJ28HtPx4DIjOcFx/KFZaKd9vZJj6pfLGygTHTCXcBQ==", null, false, "f2aa1dac-5b52-4373-8cbb-eae0a0fd2966", false, "alevelara", 1 },
                    { new Guid("d2e78011-6cb3-45a8-b330-36e963581333"), 0, "42136db1-b757-4569-8293-439e755534ea", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEGBuR6D7Sbh7KhHojlq/FqjkaPVrKI3+1Znl5ISN0t8sOy8Dlo5L2FdJdq9kV810pQ==", null, false, "6bbe81c2-30f8-4110-8bf3-cef8e9a7c171", false, "player2", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("d6db5a4f-9ccc-4095-a348-c117135ad788"), new Guid("1826f84a-0b71-4e65-a8ed-e638023b20da") },
                    { new Guid("11d5e188-78c3-4231-a283-ace535c72f68"), new Guid("25e4f5c1-3c68-4f9e-8b37-071d3c9b166f") },
                    { new Guid("c0093646-16d0-4b63-8ce1-389ec38d21ad"), new Guid("5110c289-7fbc-42bd-a77a-bf6428b3288d") },
                    { new Guid("11d5e188-78c3-4231-a283-ace535c72f68"), new Guid("d2e78011-6cb3-45a8-b330-36e963581333") }
                });
        }
    }
}
