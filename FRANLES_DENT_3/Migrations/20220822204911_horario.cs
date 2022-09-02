using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class horario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoRecomendacion",
                columns: table => new
                {
                    TipoRecomendacionId = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRecomendacion", x => x.TipoRecomendacionId);
                    table.ForeignKey(
                        name: "FK_TipoRecomendacion_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    PacienteId = table.Column<string>(nullable: false),
                    HistClinica = table.Column<int>(nullable: false),
                    TipoDocumento = table.Column<int>(nullable: false),
                    NumeroDocumento = table.Column<string>(maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(maxLength: 255, nullable: false),
                    ApellidoPaterno = table.Column<string>(maxLength: 255, nullable: false),
                    ApellidoMaterno = table.Column<string>(maxLength: 255, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "Date", nullable: false),
                    Correo = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Movil = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(maxLength: 500, nullable: true),
                    DistritoId = table.Column<string>(nullable: false),
                    Fecha_Add = table.Column<DateTime>(nullable: false),
                    TipoRecomendacionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.PacienteId);
                    table.ForeignKey(
                        name: "FK_Paciente_Distritos_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distritos",
                        principalColumn: "DistritoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paciente_TipoRecomendacion_TipoRecomendacionId",
                        column: x => x.TipoRecomendacionId,
                        principalTable: "TipoRecomendacion",
                        principalColumn: "TipoRecomendacionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_DistritoId",
                table: "Paciente",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_TipoRecomendacionId",
                table: "Paciente",
                column: "TipoRecomendacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoRecomendacion_ClinicaId",
                table: "TipoRecomendacion",
                column: "ClinicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "TipoRecomendacion");
        }
    }
}
