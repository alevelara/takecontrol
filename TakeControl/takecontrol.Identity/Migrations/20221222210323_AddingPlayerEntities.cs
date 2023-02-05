using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations;

/// <inheritdoc />
public partial class AddingPlayerEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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
                { new Guid("15063acc-fcff-4b65-b6e1-25fec68e9afb"), null, "Club", "CLUB" },
                { new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"), null, "Player", "PLAYER" },
                { new Guid("e07a73fa-ba65-402c-9a41-b88af1131edb"), null, "Administrator", "ADMINISTRATOR" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
            values: new object[,]
            {
                { new Guid("00d0e55a-0076-490a-a6e1-331b67d93543"), 0, "71041de0-771d-49a5-86e4-dc50beb782d4", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAELuWuaOkMEM0iaZDrYJRT5VqFMpy5poRwyOjkrVuBQlVNrUo2gEfD5PJqwggY/Q6yw==", null, false, "80967b11-cdcc-4f7c-91f4-4d13e1705737", false, "antgonmar2", 2 },
                { new Guid("2bb860f9-2f53-4a0c-a819-41639fec474d"), 0, "e2668296-a95d-4305-a619-ae3d272b91b1", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEPAmeTU6bDU13+8SUTM16cpxak56OaVGqaRzvEuFWnMph+mK/cz2TizYK/3hx8KPUA==", null, false, "1963da13-e057-4663-84d0-a6e68e3f10c2", false, "player2", 3 },
                { new Guid("56b6b6d7-c9bf-45d3-947f-21be0d705728"), 0, "f9970210-ea17-4d76-9686-5bfdd1cf5a8b", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEHKaOHeTOqycq3UiFPioBb+fJ+fG4jDrfK5F/i8G8Ad7t6V2f2sj4zhvAsf4HkZoWg==", null, false, "0f6e1b97-832b-4ea3-b00e-880469a3683b", false, "antgonmar", 3 },
                { new Guid("a69f006a-78b1-42e0-b78d-b4fa0d0e042b"), 0, "99ab5333-adff-475d-9e3d-230490bbd4c5", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAEN05EpWZ9ARzHAXTwxsaZxyoecayNjEKdkJ1qcjCKGt05QAdPS9rtoqICTelDosIMw==", null, false, "ccc18f64-88b0-473a-b51e-f725c74e21d0", false, "alevelara", 1 }
            });

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[,]
            {
                { new Guid("15063acc-fcff-4b65-b6e1-25fec68e9afb"), new Guid("00d0e55a-0076-490a-a6e1-331b67d93543") },
                { new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"), new Guid("2bb860f9-2f53-4a0c-a819-41639fec474d") },
                { new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"), new Guid("56b6b6d7-c9bf-45d3-947f-21be0d705728") },
                { new Guid("e07a73fa-ba65-402c-9a41-b88af1131edb"), new Guid("a69f006a-78b1-42e0-b78d-b4fa0d0e042b") }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("15063acc-fcff-4b65-b6e1-25fec68e9afb"), new Guid("00d0e55a-0076-490a-a6e1-331b67d93543") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"), new Guid("2bb860f9-2f53-4a0c-a819-41639fec474d") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"), new Guid("56b6b6d7-c9bf-45d3-947f-21be0d705728") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("e07a73fa-ba65-402c-9a41-b88af1131edb"), new Guid("a69f006a-78b1-42e0-b78d-b4fa0d0e042b") });

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("15063acc-fcff-4b65-b6e1-25fec68e9afb"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("bd858f4f-139c-4824-9db3-eb71988bef73"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("e07a73fa-ba65-402c-9a41-b88af1131edb"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("00d0e55a-0076-490a-a6e1-331b67d93543"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("2bb860f9-2f53-4a0c-a819-41639fec474d"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("56b6b6d7-c9bf-45d3-947f-21be0d705728"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("a69f006a-78b1-42e0-b78d-b4fa0d0e042b"));

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
}
