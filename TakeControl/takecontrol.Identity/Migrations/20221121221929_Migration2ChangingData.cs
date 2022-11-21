using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace takecontrol.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Migration2ChangingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21deff44-8079-4c23-a1a1-469735a517cc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7b32be5-b63b-4381-a807-3434a157cbe2", "AQAAAAIAAYagAAAAEDNglvPVzhhS1JXXWpYLvzxGv4OB0sVUejQPG94ZVX/qhlDizA3NPLVH2z8m83nHjA==", "44089e5a-432f-488d-9ff2-39b24b8ca5c8" });

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
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba1e275e-ce3f-47e9-9a26-5846987b16c3", "Alejandro", "AQAAAAIAAYagAAAAEMk6yfcILTTbG+YU/YCDWvL76k4ORH3lGMdKMKUORKmOXn/qFJeoWFznEAYIKNl6tQ==", "5de9931e-a6ed-461c-ba5b-71a5b0a9f9ec" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21deff44-8079-4c23-a1a1-469735a517cc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57a4aa86-4eac-4050-a59e-4327b4790e07", "AQAAAAIAAYagAAAAEBfvRU/Nbv13TJwRUE6CMlXbjXX0auKE9SvCp1HVHdzYXDuGllqOyc8Va0+eqyMmhQ==", "9d64257c-d9b3-43f1-9b64-df90ee8dc8f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "754ac959-d37d-400d-8b32-9ec9bea22074",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee4f9993-fa1c-4c11-855d-7e64f392f9b4", "AQAAAAIAAYagAAAAEEdmWxNJI48heE4sX4csHKzO1+vrGfKGT/Ti0kkzRgvKYaIzoRlgTCaXRL/vQBYWBQ==", "a5fa44f6-3d0b-41ad-b399-f8193dbb4f9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99475a30-d391-47bc-b38a-f63329df73b5",
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b397368-b1ca-4474-b21e-c67d5dc3b35f", "", "AQAAAAIAAYagAAAAEFt9hwYKeNoUE9JfdgXsLPpXLdJoyLJYEVRWS6A/hLvF0deacMue2V3zXibWp6YTfA==", "8601f555-ab79-46a8-ba99-61509eafe97f" });
        }
    }
}
