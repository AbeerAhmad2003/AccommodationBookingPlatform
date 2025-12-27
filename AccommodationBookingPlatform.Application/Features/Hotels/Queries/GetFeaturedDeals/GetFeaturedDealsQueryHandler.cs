using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Contracts.Services.Pricing;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Hotels.Queries.GetFeaturedDeals
{
    public class GetFeaturedDealsQueryHandler
     : IRequestHandler<GetFeaturedDealsQuery, IReadOnlyList<FeaturedHotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IFeaturedDealCalculator _dealCalculator;
        private readonly IMapper _mapper;

        public GetFeaturedDealsQueryHandler(
            IHotelRepository hotelRepository,
            IFeaturedDealCalculator dealCalculator,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _dealCalculator = dealCalculator;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<FeaturedHotelDto>> Handle(
            GetFeaturedDealsQuery request,
            CancellationToken cancellationToken)
        {
            var hotels = await _hotelRepository
                .GetFeaturedDealsAsync(request.Count, cancellationToken);

            return hotels.Select(hotel =>
            {
                var (original, discounted) =
                    _dealCalculator.CalculateBestDeal(
                        hotel,
                        DateTime.UtcNow);

                var dto = _mapper.Map<FeaturedHotelDto>(hotel);
                dto.OriginalPrice = original;
                dto.DiscountedPrice = discounted;

                return dto;
            })
                .OrderBy(dto => dto.DiscountedPrice)
                .Take(request.Count)
                .ToList();
        }
    }
}
