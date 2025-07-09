using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingTheModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Series_ContentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Series_ContentId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaved_Series_ContentId",
                table: "UserSaved");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "UserSaved",
                newName: "SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSaved_ContentId",
                table: "UserSaved",
                newName: "IX_UserSaved_SeriesId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "UserFavorites",
                newName: "SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavorites_ContentId",
                table: "UserFavorites",
                newName: "IX_UserFavorites_SeriesId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Comments",
                newName: "SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ContentId",
                table: "Comments",
                newName: "IX_Comments_SeriesId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(3730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 430, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(8760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 431, DateTimeKind.Utc).AddTicks(3700));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Series_SeriesId",
                table: "Comments",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSaved_Series_SeriesId",
                table: "UserSaved",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Series_SeriesId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaved_Series_SeriesId",
                table: "UserSaved");

            migrationBuilder.RenameColumn(
                name: "SeriesId",
                table: "UserSaved",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSaved_SeriesId",
                table: "UserSaved",
                newName: "IX_UserSaved_ContentId");

            migrationBuilder.RenameColumn(
                name: "SeriesId",
                table: "UserFavorites",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavorites_SeriesId",
                table: "UserFavorites",
                newName: "IX_UserFavorites_ContentId");

            migrationBuilder.RenameColumn(
                name: "SeriesId",
                table: "Comments",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SeriesId",
                table: "Comments",
                newName: "IX_Comments_ContentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 430, DateTimeKind.Utc).AddTicks(7370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(3730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 13, 28, 431, DateTimeKind.Utc).AddTicks(3700),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Series_ContentId",
                table: "Comments",
                column: "ContentId",
                principalTable: "Series",
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
    }
}
