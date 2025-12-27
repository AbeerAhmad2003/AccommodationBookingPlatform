using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingSnapshotToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentageApplied",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPricePerNight",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNightAtBooking",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentageApplied",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "FinalPricePerNight",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Nights",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PricePerNightAtBooking",
                table: "Bookings");
        }
    }
}
