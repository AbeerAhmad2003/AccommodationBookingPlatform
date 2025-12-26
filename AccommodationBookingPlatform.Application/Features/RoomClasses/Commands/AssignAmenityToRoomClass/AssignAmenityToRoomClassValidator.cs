using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.AssignAmenityToRoomClass
{
    public class AssignAmenityToRoomClassValidator
      : AbstractValidator<AssignAmenityToRoomClassCommand>
    {
        public AssignAmenityToRoomClassValidator()
        {
            RuleFor(x => x.RoomClassId).NotEmpty();
            RuleFor(x => x.AmenityId).NotEmpty();
        }
    }

}
