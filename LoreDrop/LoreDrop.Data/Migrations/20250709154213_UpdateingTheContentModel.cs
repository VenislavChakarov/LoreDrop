using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoreDrop.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateingTheContentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 133, DateTimeKind.Utc).AddTicks(5980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(2590));

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Contents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 134, DateTimeKind.Utc).AddTicks(190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(7750));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 133, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 9, 15, 39, 33, 343, DateTimeKind.Utc).AddTicks(7750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 9, 15, 42, 13, 134, DateTimeKind.Utc).AddTicks(190));
        }
    }
}
