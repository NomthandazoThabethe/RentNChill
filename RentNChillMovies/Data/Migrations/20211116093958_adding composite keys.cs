using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class addingcompositekeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_AspNetUsers_UserId",
                table: "MovieReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews");

            migrationBuilder.DropIndex(
                name: "IX_MovieReviews_MovieId",
                table: "MovieReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MovieReviews",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieReviews_MovieReviewId",
                table: "MovieReviews",
                column: "MovieReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_AspNetUsers_UserId",
                table: "MovieReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_AspNetUsers_UserId",
                table: "MovieReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews");

            migrationBuilder.DropIndex(
                name: "IX_MovieReviews_MovieReviewId",
                table: "MovieReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MovieReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews",
                column: "MovieReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieReviews_MovieId",
                table: "MovieReviews",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_AspNetUsers_UserId",
                table: "MovieReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
