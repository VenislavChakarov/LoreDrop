using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixingtheuserlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchLists_Series_SeriesId",
                table: "UserWatchLists");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0d7ee734-effa-4cfa-a93b-5cff69eb435c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("12c9e43f-c4f0-440a-be3d-459c57757c85"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("265a23b1-565b-41a7-abc0-d02deff44d1b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a3e1480e-caf6-41f5-9a7d-30c1bdc24e00"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f35830cb-cf21-400b-b2f4-4826f84bbc71"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("2d8f1312-2d52-4b65-bd31-509b48d319ff"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("8f43d1af-fb12-464b-b3a3-ed43beaeb391"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("e621b070-993f-4d78-a018-63f3db938781"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 28, 13, 28, 27, 477, DateTimeKind.Utc).AddTicks(2040),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(3170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 28, 13, 28, 27, 476, DateTimeKind.Utc).AddTicks(9760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5bddda65-2bf8-419c-849b-31b73ea8ded2"), "Romance" },
                    { new Guid("61d86997-ef17-4092-8e0b-1d30f8a5f7ed"), "Horror" },
                    { new Guid("9bec7d28-e81b-4851-8595-c4075c042448"), "Fantasy" },
                    { new Guid("c6a45d7c-deb3-4a78-951e-0841ab5c3482"), "Mystery" },
                    { new Guid("c772d6a2-09d7-4be2-b558-89c52c6a8dd9"), "Science Fiction" }
                });

            migrationBuilder.InsertData(
                table: "SeriesStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("40696228-95cb-40b3-af33-0b622a0fad64"), "Ongoing" },
                    { new Guid("7dbc6c55-2f13-450a-9151-231c8d661065"), "Completed" },
                    { new Guid("dc501221-26a1-42b8-ac2f-92fca769c382"), "Cancelled" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchLists_Series_SeriesId",
                table: "UserWatchLists",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWatchLists_Series_SeriesId",
                table: "UserWatchLists");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5bddda65-2bf8-419c-849b-31b73ea8ded2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("61d86997-ef17-4092-8e0b-1d30f8a5f7ed"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9bec7d28-e81b-4851-8595-c4075c042448"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c6a45d7c-deb3-4a78-951e-0841ab5c3482"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c772d6a2-09d7-4be2-b558-89c52c6a8dd9"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("40696228-95cb-40b3-af33-0b622a0fad64"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("7dbc6c55-2f13-450a-9151-231c8d661065"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: new Guid("dc501221-26a1-42b8-ac2f-92fca769c382"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(3170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 28, 13, 28, 27, 477, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(1140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 28, 13, 28, 27, 476, DateTimeKind.Utc).AddTicks(9760));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d7ee734-effa-4cfa-a93b-5cff69eb435c"), "Fantasy" },
                    { new Guid("12c9e43f-c4f0-440a-be3d-459c57757c85"), "Horror" },
                    { new Guid("265a23b1-565b-41a7-abc0-d02deff44d1b"), "Mystery" },
                    { new Guid("a3e1480e-caf6-41f5-9a7d-30c1bdc24e00"), "Romance" },
                    { new Guid("f35830cb-cf21-400b-b2f4-4826f84bbc71"), "Science Fiction" }
                });

            migrationBuilder.InsertData(
                table: "SeriesStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2d8f1312-2d52-4b65-bd31-509b48d319ff"), "Ongoing" },
                    { new Guid("8f43d1af-fb12-464b-b3a3-ed43beaeb391"), "Cancelled" },
                    { new Guid("e621b070-993f-4d78-a018-63f3db938781"), "Completed" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Series_SeriesId",
                table: "UserFavorites",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchLists_Series_SeriesId",
                table: "UserWatchLists",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
