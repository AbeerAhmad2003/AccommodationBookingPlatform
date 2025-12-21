using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator
       : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Room.RoomClassId)
                .NotEmpty();

            RuleFor(x => x.Room.Number)
                .NotEmpty()
                .MaximumLength(20);
        }
    }

}
