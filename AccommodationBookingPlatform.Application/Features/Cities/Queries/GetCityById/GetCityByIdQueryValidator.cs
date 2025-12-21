using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Queries.GetCityById
{
    public class GetCityByIdQueryValidator : AbstractValidator<GetCityByIdQuery>
    {
        public GetCityByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("City id is required.");
        }
    }

}
