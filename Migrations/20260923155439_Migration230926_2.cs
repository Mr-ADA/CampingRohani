using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampingRohani.Migrations
{
    /// <inheritdoc />
    public partial class Migration230926_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_ContactPersons_ContactPersonId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ContactPersonId",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "ContactPersonId",
                table: "Registrations");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Registrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistrationContactPersons",
                columns: table => new
                {
                    RegistrationContactPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegistrationId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPersonId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationContactPersons", x => x.RegistrationContactPersonId);
                    table.ForeignKey(
                        name: "FK_RegistrationContactPersons_ContactPersons_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPersons",
                        principalColumn: "ContactPersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationContactPersons_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationContactPersons_ContactPersonId",
                table: "RegistrationContactPersons",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationContactPersons_RegistrationId_ContactPersonId",
                table: "RegistrationContactPersons",
                columns: new[] { "RegistrationId", "ContactPersonId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationContactPersons_RegistrationId_IsPrimary",
                table: "RegistrationContactPersons",
                columns: new[] { "RegistrationId", "IsPrimary" },
                unique: true,
                filter: "[IsPrimary] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationContactPersons");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Registrations");

            migrationBuilder.AddColumn<int>(
                name: "ContactPersonId",
                table: "Registrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ContactPersonId",
                table: "Registrations",
                column: "ContactPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_ContactPersons_ContactPersonId",
                table: "Registrations",
                column: "ContactPersonId",
                principalTable: "ContactPersons",
                principalColumn: "ContactPersonId");
        }
    }
}
