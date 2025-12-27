using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.CreateDiscount
{
    public record CreateDiscountCommand(
    Guid RoomClassId,
    decimal Percentage,
    DateTime StartDateUtc,
    DateTime EndDateUtc
) : IRequest<Guid>;

}
