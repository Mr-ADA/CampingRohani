using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampingRohani.Migrations
{
    /// <inheritdoc />
    public partial class Migration230926 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ContactPersons_ContactPersonId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ContactPersonId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "Participants");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "Registrations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "Registrations");

            migrationBuilder.AddColumn<int>(
                name: "ContactPersonId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ContactPersonId",
                table: "Participants",
                column: "ContactPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_ContactPersons_ContactPersonId",
                table: "Participants",
                column: "ContactPersonId",
                principalTable: "ContactPersons",
                principalColumn: "ContactPersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
