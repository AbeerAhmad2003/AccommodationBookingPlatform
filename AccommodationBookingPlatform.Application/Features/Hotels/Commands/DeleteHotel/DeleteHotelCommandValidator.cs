using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Hotel Id is required.");
        }
    }
}
