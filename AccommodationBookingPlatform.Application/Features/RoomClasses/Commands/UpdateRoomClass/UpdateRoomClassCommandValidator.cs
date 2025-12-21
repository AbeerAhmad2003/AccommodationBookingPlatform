using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass
{
    public class UpdateRoomClassCommandValidator
       : AbstractValidator<UpdateRoomClassCommand>
    {
        public UpdateRoomClassCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.AdultsCapacity)
                .GreaterThan(0);

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0);
        }
    }
}
