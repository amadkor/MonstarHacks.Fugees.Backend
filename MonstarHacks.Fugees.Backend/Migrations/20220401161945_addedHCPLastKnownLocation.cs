using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    public partial class addedHCPLastKnownLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "LastKnownLocation",
                table: "HealthcareProfessionals",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "HealthcareProfessionals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastKnownLocation",
                table: "HealthcareProfessionals");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "HealthcareProfessionals");
        }
    }
}
