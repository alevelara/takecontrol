using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations;

/// <inheritdoc />
public partial class AddingUserId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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
                { new Guid("8ac9ecee-917c-455a-8f68-a309334ae145"), null, "Club", "CLUB" },
                { new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"), null, "Player", "PLAYER" },
                { new Guid("c2ef3a63-3e1b-483a-b41b-8f285419154d"), null, "Administrator", "ADMINISTRATOR" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
            values: new object[,]
            {
                { new Guid("1df96ba2-9473-4203-ac8f-8641b03acf7e"), 0, "8e14367b-6361-4c9d-95b7-ec016fd7cb3c", "player2@gmail.com", true, false, null, "player 2", "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEDX0DQ494/lE7HiN7vvxeAWuA7PocrRV/D1fCRMSWSOyWmjD34xJSq+WikMG+EtFUw==", null, false, "a27b0361-5efb-4399-95ff-a0f77cbb0a9c", false, "player2", 3 },
                { new Guid("41852a3d-c860-469c-99aa-ab92d65e62d0"), 0, "517ccc66-2f90-401a-b587-a0d641ebefdb", "alevelara@gmail.com", true, false, null, "Alejandro", "ALEVELARA@GMAIL.COM", "ALEVELARA", "AQAAAAIAAYagAAAAECutGUY8TBdUKPAjaZxV0k5HI/7eIUqbMGEpUYrOMn/1aam81unMl2dCNZYLaEs6Vg==", null, false, "2f066678-a7ce-43e6-8181-98cef6fe39ff", false, "alevelara", 1 },
                { new Guid("813de000-cb60-4b95-af04-efa0eb7ae5df"), 0, "285ac82d-c25b-4c6f-bc77-5c51bdaef801", "club@localhost.com", true, false, null, "PadelClubTest", "CLUB@LOCALHOST.COM", "ANTOGONMAR2", "AQAAAAIAAYagAAAAEMaIvsJoly7Kws3a0E9/fAMf2MfUMlUQcj3ATmDTjwqFgiCnB5icH3V3VPau0j/Grw==", null, false, "ab800d94-917b-40b9-b465-749cd284843a", false, "antgonmar2", 2 },
                { new Guid("d0b0eb44-4f19-4b2d-89e9-0d7db52702fa"), 0, "50259a39-c4bc-4d6a-ace0-8d647a1dae77", "alevelara@localhost.com", true, false, null, "Alberto", "ALEVELARA@LOCALHOST.COM", "ANTOGONMAR", "AQAAAAIAAYagAAAAEAO94pN9B2wJXsoomJ3T5kBJWndEnsr5oYuOyrCgol/Jq7mUXr5m1ezeJ6z0ROjfPw==", null, false, "566bea9b-acb8-4ab6-9fca-93f395c6a9ab", false, "antgonmar", 3 }
            });

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[,]
            {
                { new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"), new Guid("1df96ba2-9473-4203-ac8f-8641b03acf7e") },
                { new Guid("c2ef3a63-3e1b-483a-b41b-8f285419154d"), new Guid("41852a3d-c860-469c-99aa-ab92d65e62d0") },
                { new Guid("8ac9ecee-917c-455a-8f68-a309334ae145"), new Guid("813de000-cb60-4b95-af04-efa0eb7ae5df") },
                { new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"), new Guid("d0b0eb44-4f19-4b2d-89e9-0d7db52702fa") }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"), new Guid("1df96ba2-9473-4203-ac8f-8641b03acf7e") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("c2ef3a63-3e1b-483a-b41b-8f285419154d"), new Guid("41852a3d-c860-469c-99aa-ab92d65e62d0") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("8ac9ecee-917c-455a-8f68-a309334ae145"), new Guid("813de000-cb60-4b95-af04-efa0eb7ae5df") });

        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"), new Guid("d0b0eb44-4f19-4b2d-89e9-0d7db52702fa") });

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("8ac9ecee-917c-455a-8f68-a309334ae145"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("955cc73a-0585-44e5-83c2-ed60dbf3b8c6"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("c2ef3a63-3e1b-483a-b41b-8f285419154d"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("1df96ba2-9473-4203-ac8f-8641b03acf7e"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("41852a3d-c860-469c-99aa-ab92d65e62d0"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("813de000-cb60-4b95-af04-efa0eb7ae5df"));

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("d0b0eb44-4f19-4b2d-89e9-0d7db52702fa"));

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
}
