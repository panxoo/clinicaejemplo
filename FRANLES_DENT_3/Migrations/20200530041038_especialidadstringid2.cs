using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class especialidadstringid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Especialidad_Medicos_EspecialidadId",
                table: "Especialidad_Medicos",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Especialidad_Medicos_Especialidades_EspecialidadId",
                table: "Especialidad_Medicos",
                column: "EspecialidadId",
                principalTable: "Especialidades",
                principalColumn: "EspecialidadId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidad_Medicos_Especialidades_EspecialidadId",
                table: "Especialidad_Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Especialidad_Medicos_EspecialidadId",
                table: "Especialidad_Medicos");
        }
    }
}
