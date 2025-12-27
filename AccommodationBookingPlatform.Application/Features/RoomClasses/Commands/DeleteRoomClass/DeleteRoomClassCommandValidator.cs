using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.RoomClasses.Commands.DeleteRoomClass
{
    public class DeleteRoomClassCommandValidator
         : AbstractValidator<DeleteRoomClassCommand>
    {
        public DeleteRoomClassCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
