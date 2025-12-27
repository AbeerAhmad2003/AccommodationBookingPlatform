using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            // Primary Key
            builder.HasKey(i => i.Id);

            // Enum stored as string
            builder.Property(i => i.Type)
                .HasConversion(new EnumToStringConverter<ImageType>())
                .IsRequired();

            // Image → Hotel (optional)
            builder.HasOne(i => i.Hotel)
                .WithMany()
                .HasForeignKey(i => i.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Image → RoomClass (optional)
            builder.HasOne(i => i.RoomClass)
                .WithMany(rc => rc.Gallery)
                .HasForeignKey(i => i.RoomClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Image → City (optional)
            builder.HasOne(i => i.City)
                .WithMany()
                .HasForeignKey(i => i.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes (for fast lookup)
            builder.HasIndex(i => new { i.HotelId, i.Type });
            builder.HasIndex(i => new { i.RoomClassId, i.Type });
            builder.HasIndex(i => new { i.CityId, i.Type });
        }
    }
}
