using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetHotelDetails
{
    public record GetHotelDetailsQuery(
      Guid HotelId,
      DateTime? CheckIn,
      DateTime? CheckOut,
      int Rooms = 1
  ) : IRequest<HotelDetailsDto>;
}
