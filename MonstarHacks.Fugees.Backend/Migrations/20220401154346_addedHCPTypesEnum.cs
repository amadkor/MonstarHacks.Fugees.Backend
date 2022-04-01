using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    public partial class addedHCPTypesEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SpecialityName",
                table: "HealthcareProfessionals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpecialityName",
                table: "HealthcareProfessionals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
