using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class especialidadstringid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Especialidad_Medicos_Especialidades_EspecialidadId",
                table: "Especialidad_Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Especialidad_Medicos_EspecialidadId",
                table: "Especialidad_Medicos");

            migrationBuilder.AlterColumn<string>(
                name: "EspecialidadId",
                table: "Especialidades",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "EspecialidadId",
                table: "Especialidad_Medicos",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EspecialidadId",
                table: "Especialidades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "EspecialidadId",
                table: "Especialidad_Medicos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

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
    }
}
