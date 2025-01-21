using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cbapp.Migrations
{
    /// <inheritdoc />
    public partial class onCASCADE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "project_id");
        }
    }
}
