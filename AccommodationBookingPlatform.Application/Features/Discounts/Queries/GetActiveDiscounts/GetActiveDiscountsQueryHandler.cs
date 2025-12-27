using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Queries.GetActiveDiscounts
{
    public class GetActiveDiscountsQueryHandler
     : IRequestHandler<GetActiveDiscountsQuery, IEnumerable<ActiveDiscountDto>>
    {
        private readonly IDiscountRepository _discountRepo;
        private readonly IMapper _mapper;

        public GetActiveDiscountsQueryHandler(
            IDiscountRepository discountRepo,
            IMapper mapper)
        {
            _discountRepo = discountRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActiveDiscountDto>> Handle(
            GetActiveDiscountsQuery request,
            CancellationToken ct)
        {
            var now = DateTime.UtcNow;

            var discounts = await _discountRepo
                .FindAsync(
                    d => d.RoomClassId == request.RoomClassId &&
                         d.StartDateUtc <= now &&
                         d.EndDateUtc >= now,
                    ct);

            return _mapper.Map<IEnumerable<ActiveDiscountDto>>(discounts);
        }
    }

}
