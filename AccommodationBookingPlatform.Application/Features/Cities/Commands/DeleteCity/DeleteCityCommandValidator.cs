using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
