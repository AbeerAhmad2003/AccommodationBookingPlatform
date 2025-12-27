using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandValidator : AbstractValidator<UpdateAmenityCommand>
    {
        public UpdateAmenityCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description != null);
        }
    }

}
