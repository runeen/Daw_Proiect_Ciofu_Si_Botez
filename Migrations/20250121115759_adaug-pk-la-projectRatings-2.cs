using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cbapp.Migrations
{
    /// <inheritdoc />
    public partial class adaugpklaprojectRatings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ratingId",
                table: "ProjectRatings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ratingId",
                table: "ProjectRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
