using AccommodationBookingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationBookingPlatform.Persistence.Configurations
{
    public class InvoiceRecordConfiguration : IEntityTypeConfiguration<InvoiceRecord>
    {
        public void Configure(EntityTypeBuilder<InvoiceRecord> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Booking)
                .WithMany(b => b.InvoiceRecords)
                .HasForeignKey(i => i.BookingId);
        }
    }
}
