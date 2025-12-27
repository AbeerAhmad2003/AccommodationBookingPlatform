using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Amenities.Commands.CreateAmenity
{
    public class CreateAmenityCommandValidator : AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description != null);
        }
    }

}
