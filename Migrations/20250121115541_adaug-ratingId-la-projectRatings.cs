using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cbapp.Migrations
{
    /// <inheritdoc />
    public partial class adaugratingIdlaprojectRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ratingId",
                table: "ProjectRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ratingId",
                table: "ProjectRatings");
        }
    }
}
