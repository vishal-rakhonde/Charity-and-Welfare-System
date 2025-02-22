using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charity_and_Welfare_System.Migrations
{
    /// <inheritdoc />
    public partial class CreateCharityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Donors",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Donors",
                newName: "Password");
        }
    }
}
