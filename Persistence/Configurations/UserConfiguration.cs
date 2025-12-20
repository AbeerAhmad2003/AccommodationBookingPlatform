using AccommodationBookingPlatform.Domain.Common.Enums;
using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);
            builder.Property(u => u.PhoneNumber)
       .HasMaxLength(20)
       .IsRequired(false);

            // Enum Role stored as string
            builder.Property(u => u.Role)
                .HasConversion(new EnumToStringConverter<UserRole>())
                .IsRequired();

            // User → Bookings
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User → Reviews
            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.Guest)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
