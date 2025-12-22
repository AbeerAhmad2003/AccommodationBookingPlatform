using AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.CreateRoomClass;
using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.UpdateRoomClass
{
    public class CreateRoomClassCommandValidator
     : AbstractValidator<CreateRoomClassCommand>
    {
        public CreateRoomClassCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Hotel Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(150);

            RuleFor(x => x.AdultsCapacity)
                .GreaterThan(0).WithMessage("Adults capacity must be greater than 0.");

            RuleFor(x => x.ChildrenCapacity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than 0.");

            RuleFor(x => x.RoomType)
                .IsInEnum().WithMessage("Invalid Room Type value.");
        }
    }

}
