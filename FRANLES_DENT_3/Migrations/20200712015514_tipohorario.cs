using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class tipohorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipo_Horarios",
                columns: table => new
                {
                    Tipo_HorarioId = table.Column<string>(maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(maxLength: 500, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 1000, nullable: true),
                    Hora_Inicio = table.Column<string>(nullable: false),
                    Hora_Fin = table.Column<string>(nullable: false),
                    ClinicaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Horarios", x => x.Tipo_HorarioId);
                    table.ForeignKey(
                        name: "FK_Tipo_Horarios_Clinicas_ClinicaId",
                        column: x => x.ClinicaId,
                        principalTable: "Clinicas",
                        principalColumn: "ClinicaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Horarios_ClinicaId",
                table: "Tipo_Horarios",
                column: "ClinicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipo_Horarios");
        }
    }
}
