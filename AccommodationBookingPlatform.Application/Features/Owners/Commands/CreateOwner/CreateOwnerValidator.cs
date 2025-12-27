using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.CreateOwner
{
    public class CreateOwnerValidator : AbstractValidator<CreateOwnerCommand>
    {
        public CreateOwnerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
