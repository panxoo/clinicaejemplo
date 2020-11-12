using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class horariomedicodeldiaweek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaWeek",
                table: "HorarioMedicos");

            migrationBuilder.DropColumn(
                name: "Hora_Fin",
                table: "HorarioMedicos");

            migrationBuilder.DropColumn(
                name: "Hora_Inicio",
                table: "HorarioMedicos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaWeek",
                table: "HorarioMedicos",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora_Fin",
                table: "HorarioMedicos",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora_Inicio",
                table: "HorarioMedicos",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
