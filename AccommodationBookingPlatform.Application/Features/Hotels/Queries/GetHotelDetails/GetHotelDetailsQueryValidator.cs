using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public class GetHotelDetailsQueryValidator
        : AbstractValidator<GetHotelDetailsQuery>
    {
        public GetHotelDetailsQueryValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Hotel Id is required.");

            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .When(x => x.CheckIn.HasValue && x.CheckOut.HasValue)
                .WithMessage("Checkout date must be after check-in date.");

            RuleFor(x => x.Rooms)
                .GreaterThan(0)
                .When(x => x.Rooms > 0)
                .WithMessage("Rooms must be greater than zero if provided.");
        }
    }
}
