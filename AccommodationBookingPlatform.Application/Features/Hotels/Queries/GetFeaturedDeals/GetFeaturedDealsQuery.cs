using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals
{
    public record GetFeaturedDealsQuery(int Count = 5)
     : IRequest<IReadOnlyList<FeaturedHotelDto>>;
}
