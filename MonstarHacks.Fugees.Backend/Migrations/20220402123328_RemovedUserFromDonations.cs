using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    public partial class RemovedUserFromDonations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalSupplyDonations_Users_UserId",
                table: "MedicalSupplyDonations");

            migrationBuilder.DropIndex(
                name: "IX_MedicalSupplyDonations_UserId",
                table: "MedicalSupplyDonations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MedicalSupplyDonations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MedicalSupplyDonations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSupplyDonations_UserId",
                table: "MedicalSupplyDonations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalSupplyDonations_Users_UserId",
                table: "MedicalSupplyDonations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
