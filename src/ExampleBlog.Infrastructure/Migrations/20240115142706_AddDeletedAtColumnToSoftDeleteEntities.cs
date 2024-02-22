using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtColumnToSoftDeleteEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoftDeleteLevel",
                table: "Posts",
                newName: "DeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_SoftDeleteLevel",
                table: "Posts",
                newName: "IX_Posts_DeleteLevel");

            migrationBuilder.RenameColumn(
                name: "SoftDeleteLevel",
                table: "Comments",
                newName: "DeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SoftDeleteLevel",
                table: "Comments",
                newName: "IX_Comments_DeleteLevel");

            migrationBuilder.RenameColumn(
                name: "SoftDeleteLevel",
                table: "AspNetUsers",
                newName: "DeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_SoftDeleteLevel",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DeleteLevel");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeleteLevel",
                table: "Posts",
                newName: "SoftDeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_DeleteLevel",
                table: "Posts",
                newName: "IX_Posts_SoftDeleteLevel");

            migrationBuilder.RenameColumn(
                name: "DeleteLevel",
                table: "Comments",
                newName: "SoftDeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DeleteLevel",
                table: "Comments",
                newName: "IX_Comments_SoftDeleteLevel");

            migrationBuilder.RenameColumn(
                name: "DeleteLevel",
                table: "AspNetUsers",
                newName: "SoftDeleteLevel");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DeleteLevel",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_SoftDeleteLevel");
        }
    }
}
