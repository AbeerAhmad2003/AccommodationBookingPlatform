using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator
       : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(c => c.HotelId).NotEmpty();

            RuleFor(c => c.CheckInDate)
                .LessThan(c => c.CheckOutDate);

            RuleFor(c => c.CheckInDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date);

            RuleFor(c => c.RoomsCount)
                .GreaterThan(0);

            RuleFor(c => c.Adults)
                .GreaterThan(0);

            RuleFor(c => c.Children)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.PaymentMethod)
                .IsInEnum();
        }
    }
}
