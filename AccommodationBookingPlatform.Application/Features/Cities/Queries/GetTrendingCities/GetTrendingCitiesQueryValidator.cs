using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetTrendingCities
{
    public class GetTrendingCitiesQueryValidator
       : AbstractValidator<GetTrendingCitiesQuery>
    {
        public GetTrendingCitiesQueryValidator()
        {
            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than zero.")
                .LessThanOrEqualTo(20)
                .WithMessage("Count cannot exceed 20.");
        }
    }
}
