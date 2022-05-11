using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class updatedusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Accounts",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
