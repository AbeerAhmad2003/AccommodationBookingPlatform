using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetRecentlyVisitedHotel
{
    public class GetRecentlyVisitedHotelsQueryValidator
       : AbstractValidator<GetRecentlyVisitedHotelsQuery>
    {
        public GetRecentlyVisitedHotelsQueryValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than zero.")
                .LessThanOrEqualTo(20)
                .WithMessage("Count cannot exceed 20.");
        }
    }
}
