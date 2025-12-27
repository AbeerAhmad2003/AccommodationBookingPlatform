using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Queries.GetActiveDiscounts
{
    public record GetActiveDiscountsQuery(Guid RoomClassId)
     : IRequest<IEnumerable<ActiveDiscountDto>>;

}
