using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpperNomalizedValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"), new Guid("4e6e5429-231f-4e64-9f66-7777dfddba64") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e9af08f7-31e3-44ff-9c92-604693bc8299"), new Guid("dd83c45b-4785-4f0f-b261-a8946d2d1e30") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"), new Guid("df48c93b-a92a-4af7-870a-832f8c8c5f4e") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c3865b49-514c-4f2d-be74-c7b85966a9a4"), new Guid("f921d4ac-d8c2-4c64-aa6a-6ee3939d9163") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c3865b49-514c-4f2d-be74-c7b85966a9a4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9af08f7-31e3-44ff-9c92-604693bc8299"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4e6e5429-231f-4e64-9f66-7777dfddba64"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd83c45b-4785-4f0f-b261-a8946d2d1e30"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("df48c93b-a92a-4af7-870a-832f8c8c5f4e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f921d4ac-d8c2-4c64-aa6a-6ee3939d9163"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"), null, "Player", "Player" },
                    { new Guid("c3865b49-514c-4f2d-be74-c7b85966a9a4"), null, "Administrator", "Administrator" },
                    { new Guid("e9af08f7-31e3-44ff-9c92-604693bc8299"), null, "Club", "Club" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { new Guid("4e6e5429-231f-4e64-9f66-7777dfddba64"), 0, "ab81da97-40e6-4f88-9c3f-76a497fb981d", "player2@gmail.com", true, false, null, "player 2", "player2@gmail.com", "player2", "AQAAAAIAAYagAAAAEHnzLftb5SooY2zSLSl2xTUexFrh7l+1jMg9Sqln+/EkuaARMTlm08WS/QRLRqdWtg==", null, false, null, false, "player2", 3 },
                    { new Guid("dd83c45b-4785-4f0f-b261-a8946d2d1e30"), 0, "71cb887c-3e0b-44b9-b7b7-d82447231b9e", "club@localhost.com", true, false, null, "PadelClubTest", "club@localhost.com", "antogonmar2", "AQAAAAIAAYagAAAAEFsCMHfRO02u6M5W5AnsNs947yxLzYhQEGlLtEM57NWPLklVYw9l7V4T4vVZdYKuiA==", null, false, null, false, "antgonmar2", 2 },
                    { new Guid("df48c93b-a92a-4af7-870a-832f8c8c5f4e"), 0, "b49b825b-e65a-44d3-8185-13eadce356ca", "alevelara@localhost.com", true, false, null, "Alberto", "alevelara@localhost.com", "antogonmar", "AQAAAAIAAYagAAAAEJzNPXxDcefPYA//q2Wr8X+16kU0+H4xNvFNRvhZZ/fG1KbPTW+sL2LJICrpkpv5ww==", null, false, null, false, "antgonmar", 3 },
                    { new Guid("f921d4ac-d8c2-4c64-aa6a-6ee3939d9163"), 0, "a3670d1a-c866-43d4-89a1-678aefd8fd68", "alevelara@gmail.com", true, false, null, "Alejandro", "alevelara@gmail.com", "alevelara", "AQAAAAIAAYagAAAAENzTtxbQEom+YrH27TjkznpVS5dwm9MxAB+lt7okvaEZr5KbTvnQD3VfyFcuXBwBHw==", null, false, null, false, "alevelara", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"), new Guid("4e6e5429-231f-4e64-9f66-7777dfddba64") },
                    { new Guid("e9af08f7-31e3-44ff-9c92-604693bc8299"), new Guid("dd83c45b-4785-4f0f-b261-a8946d2d1e30") },
                    { new Guid("05dbe4d8-ba99-4670-99bb-7a9232015ddd"), new Guid("df48c93b-a92a-4af7-870a-832f8c8c5f4e") },
                    { new Guid("c3865b49-514c-4f2d-be74-c7b85966a9a4"), new Guid("f921d4ac-d8c2-4c64-aa6a-6ee3939d9163") }
                });
        }
    }
}
