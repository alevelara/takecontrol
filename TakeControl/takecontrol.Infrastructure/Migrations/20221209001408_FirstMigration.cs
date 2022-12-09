using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace takecontrol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "text", nullable: false),
                    MainAddress = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddresId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Addresses_AddresId",
                        column: x => x.AddresId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "MainAddress", "Province" },
                values: new object[,]
                {
                    { new Guid("0f1f4ac9-8738-4dfb-b161-28ba88a639ba"), "Sevilla", null, null, null, null, "address2", "Sevilla" },
                    { new Guid("78ee0ec4-5f70-4526-b3fc-3fc1d920e346"), "Sevilla", null, null, null, null, "address1", "Sevilla" },
                    { new Guid("a50105fe-6748-4d2e-a1ed-07fc1c50ceb3"), "Sevilla", null, null, null, null, "address3", "Sevilla" }
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "AddresId", "Code", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0dfc5abe-4a72-43ef-b3c7-bbe1321724b4"), new Guid("1119c834-4a1c-4c5c-8fcd-60b1ac24e418"), "0002", null, null, null, null, "Test2" },
                    { new Guid("0eb264be-68f3-4561-8da2-5ec6a4cbdf0e"), new Guid("0511ee27-22b7-4f76-b5df-e82bddb79ed9"), "0001", null, null, null, null, "Test1" },
                    { new Guid("f8be32e0-1539-4ba9-98c9-b3a47c81bdce"), new Guid("b528d2da-8edc-4573-be05-0852fbfd133c"), "0003", null, null, null, null, "Test3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_AddresId",
                table: "Clubs",
                column: "AddresId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
