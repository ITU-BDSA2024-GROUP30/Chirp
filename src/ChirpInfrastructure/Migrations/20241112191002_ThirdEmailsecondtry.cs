using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChirpInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdEmailsecondtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondEmail",
                table: "Authors",
                newName: "ThirdEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThirdEmail",
                table: "Authors",
                newName: "SecondEmail");
        }
    }
}
