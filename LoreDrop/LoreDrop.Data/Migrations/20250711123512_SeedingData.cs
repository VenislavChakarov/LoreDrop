using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(6970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(3730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 488, DateTimeKind.Utc).AddTicks(920),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Science Fiction" },
                    { 3, "Mystery" },
                    { 4, "Romance" },
                    { 5, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CreatedOn", "Description", "GenreId", "ImageUrl", "Rating", "Tittle" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7680), "A fantasy novel by J.R.R. Tolkien.", 1, "https://upload.wikimedia.org/wikipedia/en/4/4a/TheHobbit_FirstEdition.jpg", 4.7999999999999998, "The Hobbit" },
                    { 2, "Frank Herbert", new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7690), "A science fiction novel by Frank Herbert.", 2, "https://upload.wikimedia.org/wikipedia/en/a/a8/Dune_First_Edition.jpg", 4.7000000000000002, "Dune" },
                    { 3, "Bram Stoker", new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(7690), "A gothic horror novel by Bram Stoker.", 5, "https://upload.wikimedia.org/wikipedia/commons/3/35/Dracula1st.jpeg", 4.5, "Dracula" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(3730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 487, DateTimeKind.Utc).AddTicks(6970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 16, 19, 45, 240, DateTimeKind.Utc).AddTicks(8760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 11, 12, 35, 12, 488, DateTimeKind.Utc).AddTicks(920));
        }
    }
}
