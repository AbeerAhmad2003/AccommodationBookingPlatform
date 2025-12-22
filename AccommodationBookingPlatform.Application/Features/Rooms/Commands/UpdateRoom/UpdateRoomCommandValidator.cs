using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator
      : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

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
