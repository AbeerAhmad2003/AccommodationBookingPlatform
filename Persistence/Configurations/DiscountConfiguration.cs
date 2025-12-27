using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            // Primary Key
            builder.HasKey(d => d.Id);

            // Discount → RoomClass (Many-to-One)
            builder.HasOne(d => d.RoomClass)
                .WithMany(rc => rc.Discounts)
                .HasForeignKey(d => d.RoomClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Percentage (0 - 100)
            builder.Property(d => d.Percentage)
                .HasPrecision(5, 2)   // مثال: 25.50%
                .IsRequired();

            // Dates
            builder.Property(d => d.StartDateUtc)
                .IsRequired();

            builder.Property(d => d.EndDateUtc)
                .IsRequired();

            // Index for active discounts lookup
            builder.HasIndex(d => new { d.RoomClassId, d.StartDateUtc, d.EndDateUtc });
        }
    }

}
