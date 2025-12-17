using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class RoomClassConfiguration : IEntityTypeConfiguration<RoomClass>
    {
        public void Configure(EntityTypeBuilder<RoomClass> builder)
        {
            builder.HasKey(rc => rc.Id);

            // RoomClass → Hotel (Many-to-One)
            builder.HasOne(rc => rc.Hotel)
                .WithMany(h => h.RoomClasses)
                .HasForeignKey(rc => rc.HotelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Enum as string
            builder.Property(rc => rc.RoomType)
                .HasConversion(new EnumToStringConverter<RoomType>())
                .IsRequired();

            // Ignore images (loaded via Image table)
            builder.Ignore(rc => rc.Gallery);

            // RoomClass → Rooms (One-to-Many)
            builder.HasMany(rc => rc.Rooms)
                .WithOne(r => r.RoomClass)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // RoomClass ↔ Amenities (Many-to-Many)
            builder.HasMany(rc => rc.Amenities)
                .WithMany(a => a.RoomClasses);

            // Price
            builder.Property(rc => rc.PricePerNight)
                .HasPrecision(18, 2)
                .IsRequired();

            // Indexes
            builder.HasIndex(rc => rc.RoomType);
            builder.HasIndex(rc => new { rc.AdultsCapacity, rc.ChildrenCapacity });
            builder.HasIndex(rc => rc.PricePerNight);
        }
    }

}
