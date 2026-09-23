using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampingRohani.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCampContactPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantCategory",
                table: "Camps",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantCategory",
                table: "Camps");
        }
    }
}
