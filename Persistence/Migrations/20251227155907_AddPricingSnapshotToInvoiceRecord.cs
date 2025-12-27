using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingSnapshotToInvoiceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentageApplied",
                table: "InvoiceRecords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPricePerNight",
                table: "InvoiceRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "InvoiceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNightBeforeDiscount",
                table: "InvoiceRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RoomsCount",
                table: "InvoiceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentageApplied",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "FinalPricePerNight",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "Nights",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "PricePerNightBeforeDiscount",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "RoomsCount",
                table: "InvoiceRecords");
        }
    }
}
