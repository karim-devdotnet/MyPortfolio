using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItemSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ProjectName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItemSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FullName = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Profil = table.Column<string>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerSet_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OwnerSet",
                columns: new[] { "Id", "AddressId", "Avatar", "FullName", "Profil" },
                values: new object[] { new Guid("0461779c-7429-4e62-b13c-a61414dd3cda"), null, "avatar.bmp", "Abdelkarim ABDALLAH", ".Net Fullstack Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerSet_AddressId",
                table: "OwnerSet",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerSet");

            migrationBuilder.DropTable(
                name: "PortfolioItemSet");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
