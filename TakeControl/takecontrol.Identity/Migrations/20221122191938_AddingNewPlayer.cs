using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21deff44-8079-4c23-a1a1-469735a517cc",
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfa22966-b8cc-4d7b-8fe3-bde4560086c8", "Alberto", "AQAAAAIAAYagAAAAEKu58ZSPNifImFZoCn7WdLiEcsybA/CYyqaQjiLWqFtV4DB9zB2lxiUhJg0Jyaq0kg==", "55905c3d-4bd5-45b2-beea-4a4ec9470c5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "754ac959-d37d-400d-8b32-9ec9bea22074",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec9e57bc-c81c-4aba-a755-3b33f61969bc", "AQAAAAIAAYagAAAAEJHqAzDr8FLeWIV2a/C2MGm9gQherMYwvj7A/0xHQj0XoEAZVd3JA/NyDaZZTsqcKw==", "9e904591-2bb3-4e39-b575-6dfae8b05624" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99475a30-d391-47bc-b38a-f63329df73b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "369b6bed-7555-46f3-9c5a-a01e703b27d8", "AQAAAAIAAYagAAAAEO8cYrqLt8EsyiefcBTGsKdSLFQnb6CznOHZuXMGiG0LKLDYw99CGVP7/3CZnrOdlw==", "e1607e08-5928-44b1-98ba-798eb9e2b2fb" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "2ed8d389-80c8-4ef5-bce3-3c7881572379", 0, "7eccd9bb-2cc6-493e-9ca0-1e7c42735b19", "player2@gmail.com", true, false, null, "player 2", "player2@gmail.com", "player2", "AQAAAAIAAYagAAAAEM8VYothP/UOyhQt+OWZ0z18dQvjVNFgr9CGm61M2kiy+C15Hg/C9FhdTUi+rtCpUA==", null, false, "4616d921-c880-40f3-92cf-d49070b0868f", false, "player2", 3 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "57ce438d-ae66-4e90-a8d1-cf5929eaf163", "2ed8d389-80c8-4ef5-bce3-3c7881572379" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "57ce438d-ae66-4e90-a8d1-cf5929eaf163", "2ed8d389-80c8-4ef5-bce3-3c7881572379" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ed8d389-80c8-4ef5-bce3-3c7881572379");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21deff44-8079-4c23-a1a1-469735a517cc",
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7b32be5-b63b-4381-a807-3434a157cbe2", "Antonio", "AQAAAAIAAYagAAAAEDNglvPVzhhS1JXXWpYLvzxGv4OB0sVUejQPG94ZVX/qhlDizA3NPLVH2z8m83nHjA==", "44089e5a-432f-488d-9ff2-39b24b8ca5c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "754ac959-d37d-400d-8b32-9ec9bea22074",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a392af53-5462-4321-94cd-52247e5f1dcf", "AQAAAAIAAYagAAAAEAJZG2FGdAXUM9x/E0rCDEvt/nlkx0G38VB+n25p3P7X70WymHlZrxnSVDpKxH2z0g==", "3ea8e49b-00e8-4c4d-a933-c83d44b13336" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99475a30-d391-47bc-b38a-f63329df73b5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba1e275e-ce3f-47e9-9a26-5846987b16c3", "AQAAAAIAAYagAAAAEMk6yfcILTTbG+YU/YCDWvL76k4ORH3lGMdKMKUORKmOXn/qFJeoWFznEAYIKNl6tQ==", "5de9931e-a6ed-461c-ba5b-71a5b0a9f9ec" });
        }
    }
}
