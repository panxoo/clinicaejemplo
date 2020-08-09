using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class horariomedico3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora_Fin",
                table: "HorarioMedicos",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora_Inicio",
                table: "HorarioMedicos",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora_Fin",
                table: "HorarioMedicos");

            migrationBuilder.DropColumn(
                name: "Hora_Inicio",
                table: "HorarioMedicos");
        }
    }
}
