using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            // Primary Key
            builder.HasKey(o => o.Id);

            // Owner → Hotels (One-to-Many)
            builder.HasMany(o => o.Hotels)
                .WithOne(h => h.Owner)
                .HasForeignKey(h => h.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Properties
            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}
