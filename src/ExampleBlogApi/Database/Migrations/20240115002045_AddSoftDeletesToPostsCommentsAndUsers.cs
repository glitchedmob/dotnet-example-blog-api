using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleBlogApi.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletesToPostsCommentsAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "SoftDeleteLevel",
                table: "Posts",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "SoftDeleteLevel",
                table: "Comments",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "SoftDeleteLevel",
                table: "AspNetUsers",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SoftDeleteLevel",
                table: "Posts",
                column: "SoftDeleteLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SoftDeleteLevel",
                table: "Comments",
                column: "SoftDeleteLevel");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SoftDeleteLevel",
                table: "AspNetUsers",
                column: "SoftDeleteLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_SoftDeleteLevel",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SoftDeleteLevel",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SoftDeleteLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SoftDeleteLevel",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SoftDeleteLevel",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SoftDeleteLevel",
                table: "AspNetUsers");
        }
    }
}
