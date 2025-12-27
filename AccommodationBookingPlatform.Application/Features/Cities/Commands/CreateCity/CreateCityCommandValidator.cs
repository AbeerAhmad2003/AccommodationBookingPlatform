using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.Country)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.PostOffice)
                .NotEmpty().MaximumLength(20);
        }
    }

}
