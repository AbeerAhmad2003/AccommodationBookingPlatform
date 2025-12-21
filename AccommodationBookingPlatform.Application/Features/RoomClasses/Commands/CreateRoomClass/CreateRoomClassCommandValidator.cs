using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass
{
    public class CreateRoomClassCommandValidator
         : AbstractValidator<CreateRoomClassCommand>
    {
        public CreateRoomClassCommandValidator()
        {
            RuleFor(x => x.RoomClass.HotelId)
                .NotEmpty().WithMessage("Hotel Id is required.");

            RuleFor(x => x.RoomClass.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150);

            RuleFor(x => x.RoomClass.AdultsCapacity)
                .GreaterThan(0).WithMessage("Adults capacity must be greater than 0.");

            RuleFor(x => x.RoomClass.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than 0.");
        }
    }
}
