using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
    {
        public GetCitiesQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than zero.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
