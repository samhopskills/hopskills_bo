using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopSkills.BackOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationGroupUserManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationGroupApplicationUser",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroupApplicationUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationGroupApplicationUser_ApplicationGroup_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "ApplicationGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationGroupApplicationUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationGroupApplicationUser_UsersId",
                table: "ApplicationGroupApplicationUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationGroupApplicationUser");

            migrationBuilder.DropTable(
                name: "ApplicationGroup");
        }
    }
}
