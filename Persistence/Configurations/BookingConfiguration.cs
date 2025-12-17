using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.HasKey(b => b.Id);

            // Booking → User (Many-to-One)
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Hotel (Many-to-One)
            builder.HasOne(b => b.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HotelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Booking ↔ Rooms (Many-to-Many)
            builder.HasMany(b => b.Rooms)
                .WithMany(r => r.Bookings);

            // Booking → InvoiceRecords (One-to-Many)
            builder.HasMany(b => b.InvoiceRecords)
                .WithOne(ir => ir.Booking)
                .HasForeignKey(ir => ir.BookingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Enum stored as string
            builder.Property(b => b.PaymentMethod)
                .HasConversion(new EnumToStringConverter<PaymentMethod>())
                .IsRequired();

            // Price precision
            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            // Dates
            builder.Property(b => b.CheckInDate)
                .IsRequired();

            builder.Property(b => b.CheckOutDate)
                .IsRequired();

            // Index for availability / date search
            builder.HasIndex(b => new { b.CheckInDate, b.CheckOutDate });
        }
    }
}
