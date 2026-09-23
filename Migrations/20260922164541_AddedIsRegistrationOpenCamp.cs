using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampingRohani.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsRegistrationOpenCamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegistrationOpen",
                table: "Camps",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistrationOpen",
                table: "Camps");
        }
    }
}
