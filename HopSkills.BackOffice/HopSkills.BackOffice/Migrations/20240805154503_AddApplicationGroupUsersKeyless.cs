using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopSkills.BackOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationGroupUsersKeyless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupsUsers",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsUsers");
        }
    }
}
