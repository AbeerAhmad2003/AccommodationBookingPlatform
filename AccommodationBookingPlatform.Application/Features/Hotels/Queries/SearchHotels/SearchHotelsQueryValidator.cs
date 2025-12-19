using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.SearchHotels
{
    public class SearchHotelsQueryValidator
         : AbstractValidator<SearchHotelsQuery>
    {
        public SearchHotelsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than zero.")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");

            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .When(x => x.CheckIn.HasValue && x.CheckOut.HasValue)
                .WithMessage("Checkout date must be after check-in date.");

            RuleFor(x => x.Rooms)
                .GreaterThan(0)
                .When(x => x.Rooms > 0)
                .WithMessage("Rooms must be at least 1.");
        }
    }
}
