using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTheNewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 11, 22, 21, 616, DateTimeKind.Utc).AddTicks(2350),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 11, 21, 42, 117, DateTimeKind.Utc).AddTicks(5880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 11, 22, 21, 616, DateTimeKind.Utc).AddTicks(320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 11, 21, 42, 117, DateTimeKind.Utc).AddTicks(3240));

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
                table: "SeriesStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ongoing" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "Author", "CreatedOn", "Description", "GenreId", "ImageUrl", "Rating", "SeriesStateId", "Tittle" },
                values: new object[,]
                {
                    { new Guid("36f817c5-0567-4518-9bb3-e528a1d2f89a"), "Jane Doe", new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "An epic fantasy series exploring the mysteries of the LoreDrop universe.", 1, "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=800&q=80", 4.7999999999999998, null, "The Chronicles of LoreDrop" },
                    { new Guid("3e662cb1-59bf-4928-8450-d906f67b502c"), "Emily Carter", new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dive into a world where magic is real, kingdoms rise and fall, and ancient secrets wait to be discovered. Each season uncovers new lands, legendary heroes, and dark forces threatening the balance of the realms. Richly detailed lore and character-driven storytelling make this fantasy series a must-watch for genre fans.", 1, "https://images.unsplash.com/photo-1500534314209-a25ddb2bd429?auto=format&fit=crop&w=800&q=80", 4.9000000000000004, null, "Mysteries of the Forgotten Realms" },
                    { new Guid("714c8eb9-c91a-45be-909d-cfa70d5c922e"), "John Smith", new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Follow the crew of the starship Horizon as they journey through uncharted galaxies, facing cosmic threats and unraveling the secrets of ancient civilizations. This sci-fi saga blends hard science with thrilling adventure and deep philosophical questions about humanity's place in the universe.", 2, "https://images.unsplash.com/photo-1465101046530-73398c7f28ca?auto=format&fit=crop&w=800&q=80", 4.5999999999999996, null, "Spacebound: The Last Frontier" },
                    { new Guid("8c78f4bd-b968-4ab9-a58e-4748da1b8af7"), "Michael Lee", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A gripping dystopian drama set in a future where memories can be traded, stolen, and rewritten. The story follows rebels fighting against a totalitarian regime that controls the past and the future. Complex characters, moral dilemmas, and a haunting vision of technology gone awry define this series.", 3, "https://images.unsplash.com/photo-1519125323398-675f0ddb6308?auto=format&fit=crop&w=800&q=80", 4.7000000000000002, null, "Echoes of Tomorrow" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("36f817c5-0567-4518-9bb3-e528a1d2f89a"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("3e662cb1-59bf-4928-8450-d906f67b502c"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("714c8eb9-c91a-45be-909d-cfa70d5c922e"));

            migrationBuilder.DeleteData(
                table: "Series",
                keyColumn: "Id",
                keyValue: new Guid("8c78f4bd-b968-4ab9-a58e-4748da1b8af7"));

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeriesStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SeriesStates",
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
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Series",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 11, 21, 42, 117, DateTimeKind.Utc).AddTicks(5880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 11, 22, 21, 616, DateTimeKind.Utc).AddTicks(2350));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 11, 21, 42, 117, DateTimeKind.Utc).AddTicks(3240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 11, 22, 21, 616, DateTimeKind.Utc).AddTicks(320));
        }
    }
}
