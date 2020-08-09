using Microsoft.EntityFrameworkCore.Migrations;

namespace FRANLES_DENT_3.Migrations
{
    public partial class horariomedico2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "DiaWeekId",
                table: "HorarioMedicos",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaWeekId",
                table: "HorarioMedicos");
        }
    }
}
