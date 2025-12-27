using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Owners.Commands.UpdateOwner
{
    public class UpdateOwnerValidator : AbstractValidator<UpdateOwnerCommand>
    {
        public UpdateOwnerValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            When(x => x.FirstName != null, () =>
                RuleFor(x => x.FirstName).NotEmpty());

            When(x => x.LastName != null, () =>
                RuleFor(x => x.LastName).NotEmpty());

            When(x => x.PhoneNumber != null, () =>
                RuleFor(x => x.PhoneNumber).NotEmpty());
        }
    }
}
