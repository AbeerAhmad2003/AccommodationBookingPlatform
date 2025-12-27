using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.DeleteDiscount
{
    public record DeleteDiscountCommand(Guid DiscountId) : IRequest;

}
