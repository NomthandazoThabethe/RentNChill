using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class Usertablechanges30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Movies_MovieId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_MovieId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MovieId",
                table: "Transactions",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Movies_MovieId",
                table: "Transactions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
