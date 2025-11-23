using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfilSena.API.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PabloReyes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PabloReyes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PabloReyesEmisorId = table.Column<int>(type: "int", nullable: false),
                    PabloReyesReceptorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_PabloReyes_PabloReyesEmisorId",
                        column: x => x.PabloReyesEmisorId,
                        principalTable: "PabloReyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_PabloReyes_PabloReyesReceptorId",
                        column: x => x.PabloReyesReceptorId,
                        principalTable: "PabloReyes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PabloReyesEmisorId_PabloReyesReceptorId",
                table: "Comentarios",
                columns: new[] { "PabloReyesEmisorId", "PabloReyesReceptorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PabloReyesReceptorId",
                table: "Comentarios",
                column: "PabloReyesReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_PabloReyes_Nombre",
                table: "PabloReyes",
                column: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "PabloReyes");
        }
    }
}
