namespace AccommodationBookingPlatform.Domain.Common
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
