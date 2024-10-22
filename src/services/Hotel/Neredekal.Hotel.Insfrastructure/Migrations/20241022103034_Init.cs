using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neredekal.Hotel.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonName = table.Column<string>(type: "text", nullable: false),
                    PersonSurname = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "HotelContactInfoItems",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationType = table.Column<int>(type: "integer", nullable: false),
                    InformationContent = table.Column<string>(type: "text", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelContactInfoItems", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_HotelContactInfoItems_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelContactInfoItems_HotelId",
                table: "HotelContactInfoItems",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelContactInfoItems");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
