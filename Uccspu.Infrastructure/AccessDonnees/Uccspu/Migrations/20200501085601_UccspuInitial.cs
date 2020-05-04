using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uccspu.Infrastructure.AccessDonnees.Uccspu.Migrations
{
    public partial class UccspuInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreeLe = table.Column<DateTime>(nullable: false),
                    CreePar = table.Column<string>(nullable: true),
                    ModifieLe = table.Column<DateTime>(nullable: true),
                    ModifiePar = table.Column<string>(nullable: true),
                    Libelle = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episodes");
        }
    }
}
