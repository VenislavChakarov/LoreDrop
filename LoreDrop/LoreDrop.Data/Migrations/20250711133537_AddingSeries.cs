using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(5430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(6970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 13, 35, 36, 993, DateTimeKind.Utc).AddTicks(1290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 488, DateTimeKind.Utc).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(6690));

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CreatedOn", "Description", "GenreId", "ImageUrl", "Rating", "Tittle" },
                values: new object[] { 4, "Jim Theis", new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(6690), "A science fiction novella often cited as one of the worst works of literature ever published.", 3, "https://upload.wikimedia.org/wikipedia/en/3/3f/Eye_of_Argon.jpg", 1.5, "The Eye of Argon" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(6970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 13, 35, 36, 992, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 488, DateTimeKind.Utc).AddTicks(920),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 13, 35, 36, 993, DateTimeKind.Utc).AddTicks(1290));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7690));
        }
    }
}
