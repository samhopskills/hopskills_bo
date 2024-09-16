using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopSkills.BackOffice.Migrations
{
    /// <inheritdoc />
    public partial class updateApplicationGameProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DifficultyLevel",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ElligibleSub",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PriorGame",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ElligibleSub",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PriorGame",
                table: "Games");
        }
    }
}
