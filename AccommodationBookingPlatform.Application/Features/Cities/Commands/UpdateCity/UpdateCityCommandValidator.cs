using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.Country)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.PostOffice)
                .NotEmpty().MaximumLength(20);
        }
    }
}
