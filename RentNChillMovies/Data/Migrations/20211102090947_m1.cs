using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewRating",
                table: "MovieReviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewRating",
                table: "MovieReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
