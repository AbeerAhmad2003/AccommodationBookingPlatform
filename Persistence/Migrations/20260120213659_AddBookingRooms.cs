using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccommodationBookingPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords");

            migrationBuilder.DropTable(
                name: "BookingRoom");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceRecords_RoomId",
                table: "InvoiceRecords");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "InvoiceRecords");

            migrationBuilder.CreateTable(
                name: "BookingRooms",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRooms", x => new { x.BookingId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_BookingRooms_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_RoomId",
                table: "BookingRooms",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRooms");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "InvoiceRecords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingRoom",
                columns: table => new
                {
                    BookingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRoom", x => new { x.BookingsId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_BookingRoom_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRecords_RoomId",
                table: "InvoiceRecords",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoom_RoomsId",
                table: "BookingRoom",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRecords_Rooms_RoomId",
                table: "InvoiceRecords",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
