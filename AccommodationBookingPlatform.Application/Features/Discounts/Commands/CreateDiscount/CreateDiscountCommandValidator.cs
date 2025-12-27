using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.RoomClassId).NotEmpty();

            RuleFor(x => x.Percentage)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.EndDateUtc)
                .GreaterThan(x => x.StartDateUtc)
                .WithMessage("End date must be after start date.");
        }
    }

}
