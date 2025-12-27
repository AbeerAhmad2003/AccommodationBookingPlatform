using AccommodationBookingPlatform.Application.Features.Discounts.Common;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.UpdateDiscount
{
    public record UpdateDiscountCommand(
    decimal Percentage,
    DateTime StartDateUtc,
    DateTime EndDateUtc
) : IRequest<DiscountDto>
    {
        public Guid Id { get; init; }
    }

}
