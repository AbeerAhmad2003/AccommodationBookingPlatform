using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomClassToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "DiscountPercentageAtBooking",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "RoomClassName",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "InvoiceRecords");

            migrationBuilder.RenameColumn(
                name: "PriceAtBooking",
                table: "InvoiceRecords",
                newName: "TotalAmount");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "InvoiceRecords",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "InvoiceRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "InvoiceRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtUtc",
                table: "InvoiceRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "InvoiceRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "InvoiceRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoomClassId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomClassId",
                table: "Bookings",
                column: "RoomClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomClasses_RoomClassId",
                table: "Bookings",
                column: "RoomClassId",
                principalTable: "RoomClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomClasses_RoomClassId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomClassId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "ModifiedAtUtc",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "RoomClassId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "InvoiceRecords",
                newName: "PriceAtBooking");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "InvoiceRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentageAtBooking",
                table: "InvoiceRecords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomClassName",
                table: "InvoiceRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "InvoiceRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
