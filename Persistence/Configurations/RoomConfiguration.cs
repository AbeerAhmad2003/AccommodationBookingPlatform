using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);

            // Room → RoomClass (Many-to-One)
            builder.HasOne(r => r.RoomClass)
                .WithMany(rc => rc.Rooms)
                .HasForeignKey(r => r.RoomClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.BookingRooms)
    .WithOne(br => br.Room)
    .HasForeignKey(br => br.RoomId)
    .IsRequired()
    .OnDelete(DeleteBehavior.Cascade);

            // Room Number
            builder.Property(r => r.Number)
                .IsRequired()
                .HasMaxLength(20);

            // Indexes
            builder.HasIndex(r => new { r.RoomClassId, r.Number })
                .IsUnique();
        }
    }

}
