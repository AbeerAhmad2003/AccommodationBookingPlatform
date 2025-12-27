using AccommodationBookingPlatform.Application.Contracts.Persistence;
using AccommodationBookingPlatform.Application.Exceptions;
using AccommodationBookingPlatform.Application.Features.Discounts.Common;
using AutoMapper;
using MediatR;

namespace AccommodationBookingPlatform.Application.Features.Discounts.Commands.UpdateDiscount
{
    public class UpdateDiscountCommandHandler
    : IRequestHandler<UpdateDiscountCommand, DiscountDto>
    {
        private readonly IDiscountRepository _repo;
        private readonly IMapper _mapper;

        public UpdateDiscountCommandHandler(IDiscountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DiscountDto> Handle(UpdateDiscountCommand request, CancellationToken ct)
        {
            var discount = await _repo.GetByIdAsync(request.Id, ct);

            if (discount is null)
                throw new NotFoundException("Discount", request.Id);

            discount.Percentage = request.Percentage;
            discount.StartDateUtc = request.StartDateUtc;
            discount.EndDateUtc = request.EndDateUtc;

            await _repo.UpdateAsync(discount, ct);

            return _mapper.Map<DiscountDto>(discount);
        }
    }

}
