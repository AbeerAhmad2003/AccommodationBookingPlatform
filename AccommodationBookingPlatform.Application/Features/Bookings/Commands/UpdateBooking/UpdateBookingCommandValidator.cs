using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty()
                .WithMessage("Booking Id is required.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty()
                .WithMessage("Check-in date is required.")
                .LessThan(x => x.CheckOutDate)
                .WithMessage("Check-in date must be before check-out date.")
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Check-in date must be in the future.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty()
                .WithMessage("Check-out date is required.");

            RuleFor(x => x.RoomsCount)
                .GreaterThan(0)
                .WithMessage("Rooms count must be greater than zero.");

            RuleFor(x => x.Adults)
                .GreaterThan(0)
                .WithMessage("At least one adult is required.");

            RuleFor(x => x.Children)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Children count cannot be negative.");
        }
    }

}
