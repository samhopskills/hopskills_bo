using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopSkills.BackOffice.Migrations
{
    /// <inheritdoc />
    public partial class TrainingContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationTrainingId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDuration = table.Column<TimeOnly>(type: "time", nullable: false),
                    DoesCertify = table.Column<bool>(type: "bit", nullable: false),
                    TotalXperience = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsGames",
                columns: table => new
                {
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ApplicationTrainingId",
                table: "Games",
                column: "ApplicationTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CreatorId",
                table: "Trainings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CustomerId",
                table: "Trainings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Trainings_ApplicationTrainingId",
                table: "Games",
                column: "ApplicationTrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Trainings_ApplicationTrainingId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "TrainingsGames");

            migrationBuilder.DropIndex(
                name: "IX_Games_ApplicationTrainingId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ApplicationTrainingId",
                table: "Games");
        }
    }
}
