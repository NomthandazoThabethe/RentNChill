using Microsoft.EntityFrameworkCore.Migrations;

namespace RentNChillMovies.Data.Migrations
{
    public partial class UserLikesComposite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes");

            migrationBuilder.DropIndex(
                name: "IX_UserLikes_MovieId",
                table: "UserLikes");

            migrationBuilder.DropColumn(
                name: "UserLikesId",
                table: "UserLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLikes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserLikes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "UserLikesId",
                table: "UserLikes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes",
                column: "UserLikesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_MovieId",
                table: "UserLikes",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_AspNetUsers_UserId",
                table: "UserLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
