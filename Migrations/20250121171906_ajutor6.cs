using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cbapp.Migrations
{
    /// <inheritdoc />
    public partial class ajutor6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs");

            migrationBuilder.AlterColumn<string>(
                name: "length",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AlterColumn<string>(
                name: "length",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_projects_project_id",
                table: "Songs",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "project_id");
        }
    }
}
