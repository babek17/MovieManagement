using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagement.Migrations
{
    /// <inheritdoc />
    public partial class RatingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Movies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Movies");
            
        }
    }
}
