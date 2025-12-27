using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator
     : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.RoomClassId)
                .NotEmpty()
                .WithMessage("Room class id is required.");

            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Room number is required.")
                .MaximumLength(20)
                .WithMessage("Room number cannot exceed 20 characters.");
        }
    }


}
