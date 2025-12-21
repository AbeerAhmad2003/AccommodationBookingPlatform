using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryValidator : AbstractValidator<GetHotelsQuery>
    {
        public GetHotelsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
