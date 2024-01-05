using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameAndImprovePasswordRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Passwords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Passwords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
