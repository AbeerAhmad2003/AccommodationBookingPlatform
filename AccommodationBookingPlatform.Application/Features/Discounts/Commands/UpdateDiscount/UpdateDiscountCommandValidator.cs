using FluentValidation;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.UpdateDiscount
{
    public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
    {
        public UpdateDiscountCommandValidator()
        {
            RuleFor(x => x.Percentage)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.EndDateUtc)
                .GreaterThan(x => x.StartDateUtc);
        }
    }

}
