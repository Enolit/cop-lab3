using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsApp3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScholarshipToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsScholarship",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "Scholarship",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scholarship",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "IsScholarship",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
