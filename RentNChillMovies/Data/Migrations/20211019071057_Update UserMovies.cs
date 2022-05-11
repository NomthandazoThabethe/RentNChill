using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class UpdateUserMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_Movies_MovieId",
                table: "UserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_AspNetUsers_UserId",
                table: "UserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovie");

            migrationBuilder.DropColumn(
                name: "UserMovieId",
                table: "UserMovie");

            migrationBuilder.RenameTable(
                name: "UserMovie",
                newName: "UserMovies");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovie_UserId",
                table: "UserMovies",
                newName: "IX_UserMovies_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMovies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies");

            migrationBuilder.RenameTable(
                name: "UserMovies",
                newName: "UserMovie");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_UserId",
                table: "UserMovie",
                newName: "IX_UserMovie_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMovie",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "UserMovieId",
                table: "UserMovie",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie",
                column: "UserMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovie",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_Movies_MovieId",
                table: "UserMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_AspNetUsers_UserId",
                table: "UserMovie",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
