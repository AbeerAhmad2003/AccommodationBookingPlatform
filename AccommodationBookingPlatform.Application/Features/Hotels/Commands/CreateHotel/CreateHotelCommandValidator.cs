using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(200);

            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.OwnerId).NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180);

            RuleFor(x => x.BriefDescription)
                .MaximumLength(500);

            RuleFor(x => x.Description)
                .MaximumLength(4000);
        }
    }

}
