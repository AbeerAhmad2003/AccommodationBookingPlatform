using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.RemoveAmenityFromRoomClass
{
    public class RemoveAmenityFromRoomClassValidator
     : AbstractValidator<RemoveAmenityFromRoomClassCommand>
    {
        public RemoveAmenityFromRoomClassValidator()
        {
            RuleFor(x => x.RoomClassId).NotEmpty();
            RuleFor(x => x.AmenityId).NotEmpty();
        }
    }

}
