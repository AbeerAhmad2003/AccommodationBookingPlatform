using AccommodationBookingPlatform.Domain.Entities.AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class BookingRoomConfiguration : IEntityTypeConfiguration<BookingRoom>
    {
        public void Configure(EntityTypeBuilder<BookingRoom> builder)
        {
            builder.HasKey(x => new { x.BookingId, x.RoomId });

            builder.HasOne(x => x.Booking)
                .WithMany(b => b.BookingRooms)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Room)
                .WithMany(r => r.BookingRooms)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAtUtc).IsRequired();
        }
    }
}
