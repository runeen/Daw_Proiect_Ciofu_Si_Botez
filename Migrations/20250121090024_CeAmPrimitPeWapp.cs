using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cbapp.Migrations
{
    /// <inheritdoc />
    public partial class CeAmPrimitPeWapp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "songRatings");

            migrationBuilder.DropIndex(
                name: "IX_Songs_title",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "ProjectRatings",
                columns: table => new
                {
                    projectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    rating_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    score = table.Column<decimal>(type: "decimal(4,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRatings", x => new { x.projectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRatings_projects_projectId",
                        column: x => x.projectId,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRatings_UserId",
                table: "ProjectRatings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRatings");

            migrationBuilder.CreateTable(
                name: "songRatings",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    rating_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    score = table.Column<decimal>(type: "decimal(4,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songRatings", x => new { x.SongId, x.UserId });
                    table.ForeignKey(
                        name: "FK_songRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_songRatings_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "song_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_title",
                table: "Songs",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_songRatings_UserId",
                table: "songRatings",
                column: "UserId");
        }
    }
}
