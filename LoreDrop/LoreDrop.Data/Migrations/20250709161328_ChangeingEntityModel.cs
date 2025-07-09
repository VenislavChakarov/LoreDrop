using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeingEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Genres_GenreId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Contents_ContentId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaved_Contents_ContentId",
                table: "UserSaved");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Series");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_GenreId",
                table: "Series",
                newName: "IX_Series_GenreId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 431, DateTimeKind.Utc).AddTicks(3700),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 134, DateTimeKind.Utc).AddTicks(190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 430, DateTimeKind.Utc).AddTicks(7370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 133, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Series_ContentId",
                table: "Comments",
                column: "ContentId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Series_ContentId",
                table: "UserFavorites",
                column: "ContentId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSaved_Series_ContentId",
                table: "UserSaved",
                column: "ContentId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Series_ContentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Genres_GenreId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Series_ContentId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaved_Series_ContentId",
                table: "UserSaved");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.RenameTable(
                name: "Series",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_Series_GenreId",
                table: "Contents",
                newName: "IX_Contents_GenreId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 134, DateTimeKind.Utc).AddTicks(190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 431, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 133, DateTimeKind.Utc).AddTicks(5980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 430, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Contents_ContentId",
                table: "Comments",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Genres_GenreId",
                table: "Contents",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Contents_ContentId",
                table: "UserFavorites",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSaved_Contents_ContentId",
                table: "UserSaved",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
