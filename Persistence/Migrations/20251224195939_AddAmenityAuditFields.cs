using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenityAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Amenities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtUtc",
                table: "Amenities",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "ModifiedAtUtc",
                table: "Amenities");
        }
    }
}
