using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty()
                .WithMessage("Booking Id is required.");
        }
    }
}
