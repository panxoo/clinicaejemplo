using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class HorarioMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hora_Inicio",
                table: "Tipo_Horarios",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hora_Fin",
                table: "Tipo_Horarios",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.CreateTable(
                name: "HorarioMedicos",
                columns: table => new
                {
                    HorarioMedicoId = table.Column<string>(maxLength: 255, nullable: false),
                    DiaWeek = table.Column<string>(maxLength: 20, nullable: true),
                    Tipo_HorarioId = table.Column<string>(maxLength: 255, nullable: true),
                    SucursalId = table.Column<string>(maxLength: 255, nullable: true),
                    UsuarioId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioMedicos", x => x.HorarioMedicoId);
                    table.ForeignKey(
                        name: "FK_HorarioMedicos_Sucursals_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursals",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorarioMedicos_Tipo_Horarios_Tipo_HorarioId",
                        column: x => x.Tipo_HorarioId,
                        principalTable: "Tipo_Horarios",
                        principalColumn: "Tipo_HorarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorarioMedicos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorarioMedicoAreaAtencions",
                columns: table => new
                {
                    Area_AtencionId = table.Column<string>(maxLength: 255, nullable: false),
                    HorarioMedicoId = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioMedicoAreaAtencions", x => new { x.Area_AtencionId, x.HorarioMedicoId });
                    table.ForeignKey(
                        name: "FK_HorarioMedicoAreaAtencions_Area_Atencions_Area_AtencionId",
                        column: x => x.Area_AtencionId,
                        principalTable: "Area_Atencions",
                        principalColumn: "Area_AtencionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioMedicoAreaAtencions_HorarioMedicos_HorarioMedicoId",
                        column: x => x.HorarioMedicoId,
                        principalTable: "HorarioMedicos",
                        principalColumn: "HorarioMedicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedicoAreaAtencions_HorarioMedicoId",
                table: "HorarioMedicoAreaAtencions",
                column: "HorarioMedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedicos_SucursalId",
                table: "HorarioMedicos",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedicos_Tipo_HorarioId",
                table: "HorarioMedicos",
                column: "Tipo_HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioMedicos_UsuarioId",
                table: "HorarioMedicos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioMedicoAreaAtencions");

            migrationBuilder.DropTable(
                name: "HorarioMedicos");

            migrationBuilder.AlterColumn<string>(
                name: "Hora_Inicio",
                table: "Tipo_Horarios",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "Hora_Fin",
                table: "Tipo_Horarios",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
