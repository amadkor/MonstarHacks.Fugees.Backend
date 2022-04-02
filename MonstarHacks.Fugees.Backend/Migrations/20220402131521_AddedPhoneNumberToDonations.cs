using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonstarHacks.Fugees.Backend.Migrations
{
    public partial class AddedPhoneNumberToDonations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "MedicalSupplyDonations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "MedicalSupplyDonations");
        }
    }
}
