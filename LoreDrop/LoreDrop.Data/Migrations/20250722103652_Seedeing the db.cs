using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seedeingthedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(3170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 33, 21, 533, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(1140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 33, 21, 533, DateTimeKind.Utc).AddTicks(5690));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: new DateTime(2025, 7, 22, 10, 33, 21, 533, DateTimeKind.Utc).AddTicks(7670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(3170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 22, 10, 33, 21, 533, DateTimeKind.Utc).AddTicks(5690),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 22, 10, 36, 52, 199, DateTimeKind.Utc).AddTicks(1140));
        }
    }
}
