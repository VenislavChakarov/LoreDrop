using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 6, 29, 12, 20, 25, 820, DateTimeKind.Utc).AddTicks(2190));

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(7750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 6, 29, 12, 20, 25, 820, DateTimeKind.Utc).AddTicks(6360));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Contents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 29, 12, 20, 25, 820, DateTimeKind.Utc).AddTicks(2190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(2590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 29, 12, 20, 25, 820, DateTimeKind.Utc).AddTicks(6360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(7750));
        }
    }
}
