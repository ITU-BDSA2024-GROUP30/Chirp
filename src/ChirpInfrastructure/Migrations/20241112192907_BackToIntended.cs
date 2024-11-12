using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChirpInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BackToIntended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteColor",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ThirdEmail",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteColor",
                table: "Authors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdEmail",
                table: "Authors",
                type: "TEXT",
                nullable: true);
        }
    }
}
