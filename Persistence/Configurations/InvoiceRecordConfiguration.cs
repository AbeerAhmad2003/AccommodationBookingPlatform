using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class InvoiceRecordConfiguration
    {
        public void Configure(EntityTypeBuilder<InvoiceRecord> builder)
        {
            // Primary Key
            builder.HasKey(ir => ir.Id);

            // InvoiceRecord → Booking (Many-to-One)
            builder.HasOne(ir => ir.Booking)
                .WithMany(b => b.InvoiceRecords)
                .HasForeignKey(ir => ir.BookingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Room info snapshot
            builder.Property(ir => ir.RoomClassName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ir => ir.RoomNumber)
                .IsRequired()
                .HasMaxLength(20);

            // Prices at booking time
            builder.Property(ir => ir.PriceAtBooking)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(ir => ir.DiscountPercentageAtBooking)
                .HasPrecision(5, 2);
        }
    }
}
