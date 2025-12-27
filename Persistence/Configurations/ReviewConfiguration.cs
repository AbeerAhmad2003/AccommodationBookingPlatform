using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);

            // Review → Hotel (Many-to-One)
            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Review → User (Guest) (Many-to-One)
            builder.HasOne(r => r.Guest)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.GuestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Content
            builder.Property(r => r.Content)
                .IsRequired()
                .HasMaxLength(1000);

            // Rating (enum)
            builder.Property(r => r.Rating)
                .IsRequired();

            // Indexes (performance)
            builder.HasIndex(r => r.HotelId);
            builder.HasIndex(r => r.GuestId);
        }
    }

}
