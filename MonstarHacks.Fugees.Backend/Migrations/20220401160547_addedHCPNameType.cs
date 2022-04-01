using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    public partial class addedHCPNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialityName",
                table: "HealthcareProfessionals",
                newName: "SpecialityId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HealthcareProfessionals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HealthcareProfessionalSpecialtyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareProfessionalSpecialtyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthcareProfessionals_SpecialityId",
                table: "HealthcareProfessionals",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthcareProfessionals_HealthcareProfessionalSpecialtyTypes_SpecialityId",
                table: "HealthcareProfessionals",
                column: "SpecialityId",
                principalTable: "HealthcareProfessionalSpecialtyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthcareProfessionals_HealthcareProfessionalSpecialtyTypes_SpecialityId",
                table: "HealthcareProfessionals");

            migrationBuilder.DropTable(
                name: "HealthcareProfessionalSpecialtyTypes");

            migrationBuilder.DropIndex(
                name: "IX_HealthcareProfessionals_SpecialityId",
                table: "HealthcareProfessionals");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "HealthcareProfessionals");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "HealthcareProfessionals",
                newName: "SpecialityName");
        }
    }
}
