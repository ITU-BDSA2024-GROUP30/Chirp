using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChirpInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteColor",
                table: "Authors",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteColor",
                table: "Authors");
        }
    }
}
